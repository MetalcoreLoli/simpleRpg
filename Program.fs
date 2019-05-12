// Learn more about F# at http://fsharp.org

open System
open SFML.Graphics
open SFML.Window
open Core
open Actors
open SFML.System
open MessageBox

                      
                                
[<EntryPoint;STAThread>]
let main argv =
    let gameWin = new RenderWindow(new VideoMode((uint32)800, (uint32)600), "testGameTwo")

    let player = new Actors.Player()
    let slime = new Actors.Slime()
    let slimeOne = new Actors.Slime()
    
    gameWin.SetVerticalSyncEnabled(true)
    gameWin.Closed.AddHandler(fun sender evArgs -> (sender :?> RenderWindow).Close())

    let rect = new RectangleShape()
    let bullet = new RectangleShape()

    bullet.Size <- new Vector2f(16.0f, 16.0f)
    bullet.FillColor <- SFML.Graphics.Color((byte)0, (byte)255, (byte)25)

    rect.Size <- new Vector2f(32.0f, 32.0f)
            
    let playercord = new Text("player X: "+(player :> IActor).X.ToString()+" player Y: "+(player :> IActor).Y.ToString(), Content.Font)
    
    playercord.FillColor <- SFML.Graphics.Color.Red
    playercord.Position <- new Vector2f(150.0f, 0.0f)
    playercord.CharacterSize <- 5u

    let playerHealth = new Text("", Content.Font)
    playerHealth.CharacterSize <- 15u
    
    playerHealth.FillColor <- SFML.Graphics.Color((byte)155, (byte)255, (byte)25)
    playerHealth.OutlineColor <- SFML.Graphics.Color.Black
    
    playerHealth.OutlineThickness <- 2.0f
    
    let statusString = new Text(player.StatusString, Content.Font)
    
    statusString.FillColor <- SFML.Graphics.Color.Yellow
    
    playercord.CharacterSize <- 15u
            
    let smilecord = new Text("HP: "+(slime :> IActor).Hp.ToString(), Content.Font)
    smilecord.Position  <- new Vector2f(200.0f, 50.0f)
    smilecord.CharacterSize <- 15u
    smilecord.OutlineThickness <- 2.0f
    
    let clock =  new Clock()
    while gameWin.IsOpen do
        
        gameWin.DispatchEvents()
        gameWin.Clear()
        let mutable time = clock.ElapsedTime.AsMilliseconds()
        clock.Restart() |> ignore
        offsetX <- (player :> IActor).X - 600.0f / 2.0f
        offsetY <- (player:> IActor).Y - 800.0f / 2.0f
        gameWin.SetTitle("Level: "+level.ToString())
        
        //slime.move(1)
         
        slime.Update((float32)time)   
        smilecord.Position  <- new Vector2f((slime :> IActor).X - offsetX, (slime :> IActor).Y - 32.0f - offsetY)     
        smilecord.DisplayedString <- "HP: "+(slime :> IActor).Hp.ToString()+"\nx:"+((int)((slime :>IActor).X / 32.0f)).ToString() + " y:"+((int)((slime :> IActor).Y / 32.0f)).ToString()
        slimeOne.Update((float32)time)
        
        //if Keyboard.IsKeyPressed(Keyboard.Key.LShift) then 
        if Keyboard.IsKeyPressed(Keyboard.Key.Left) then player.move(Keyboard.Key.Left)                                              
        if Keyboard.IsKeyPressed(Keyboard.Key.Up) then player.move(Keyboard.Key.Up)
        
        if Keyboard.IsKeyPressed(Keyboard.Key.Right) then player.move(Keyboard.Key.Right)
        
        if Keyboard.IsKeyPressed(Keyboard.Key.Down) then player.move(Keyboard.Key.Down)
        if Keyboard.IsKeyPressed(Keyboard.Key.Escape) then gameWin.Close()

        playerHealth.Position <- new Vector2f((player :> IActor).X - offsetX, (player :> IActor).Y - 40.0f - offsetY)
        playerHealth.DisplayedString <- player.StatusString+"x: "+((int)((player :>IActor).X / 32.0f)).ToString()+" y: "+((int)((player :> IActor).Y / 32.0f)).ToString()
        
        if (player :> IActor).IsLive then
                                     player.Update((float32)time)
                                                
                  
                      
                    
                
        playercord.DisplayedString <- "player X: "+(player :> IActor).X.ToString()+" player Y: "+(player :> IActor).Y.ToString()
        
        statusString.DisplayedString <- player.StatusString
                     //draw here!!!
        
        for i = 0 to H - 1  do
                            for j = 0 to W - 1  do 
                                             if tileMap.[i].[j] = '#' then rect.FillColor <- SFML.Graphics.Color.White
                                             if tileMap.[i].[j] = '-' then rect.FillColor <- SFML.Graphics.Color.Transparent
                                             if tileMap.[i].[j] = 'T' then rect.FillColor <- SFML.Graphics.Color.Red
                                             if tileMap.[i].[j] = 'N' then rect.FillColor <- SFML.Graphics.Color.Blue
                                             if tileMap.[i].[j] = 'P' then rect.FillColor <- SFML.Graphics.Color.Yellow
                                             if tileMap.[i].[j] = 's' then
                                                                         (slimeOne :> IActor).Sprite.Position <- new Vector2f((float32)j * 32.0f - offsetX, (float32)i * 32.0f - offsetY)
                                                                         (slimeOne :> IActor).Sprite.Color <- SFML.Graphics.Color.Red
                                                                         tileMap.[i] <- tileMap.[i].Replace(tileMap.[i].[j], '-')
                                                                         

                                             rect.Position <- new Vector2f((float32)j * 32.0f - offsetX, (float32)i * 32.0f - offsetY)
                                             gameWin.Draw(rect)
    
        if not (player :> IActor).IsLive then MessageBox.Show("You Dead!", gameWin)
        
        gameWin.Draw((player :> IActor).Sprite)
        gameWin.Draw(smilecord)
        gameWin.Draw((slimeOne :> IActor).Sprite)
        gameWin.Draw((slime :> IActor).Sprite)
        gameWin.Draw(playerHealth)
        
        gameWin.Display()
         
    0 // return an integer exit code
