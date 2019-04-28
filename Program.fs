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
    gameWin.SetVerticalSyncEnabled(true)
    gameWin.Closed.AddHandler(fun sender evArgs -> (sender :?> RenderWindow).Close())

    let rect = new RectangleShape()
    
    rect.Size <- new Vector2f(32.0f, 32.0f)
            
    let playercord = new Text("player X: "+(player :> IActor).X.ToString()+" player Y: "+(player :> IActor).Y.ToString(), Content.Font)
    playercord.FillColor <- SFML.Graphics.Color.Red
    playercord.Position <- new Vector2f(150.0f, 0.0f)
            
    let statusString = new Text(player.StatusString, Content.Font)
    statusString.FillColor <- SFML.Graphics.Color.Yellow
            
    let smilecord = new Text("slime X: "+(slime :> IActor).X.ToString()+" slime Y: "+(slime :> IActor).Y.ToString(), Content.Font)
    smilecord.Position  <- new Vector2f(200.0f, 50.0f)
            
    let rec mainLoop() = 
                       gameWin.DispatchEvents()
                       let time = DateTime.Now.Millisecond
                       offsetX <- (player :> IActor).X -  600.0f / 2.0f
                       offsetY <- (player:> IActor).Y -  800.0f / 2.0f
    
                       if Keyboard.IsKeyPressed(Keyboard.Key.Left) then player.move(Keyboard.Key.Left)
                       if Keyboard.IsKeyPressed(Keyboard.Key.Up) then player.move(Keyboard.Key.Up)
                       if Keyboard.IsKeyPressed(Keyboard.Key.Right) then player.move(Keyboard.Key.Right)
                       if Keyboard.IsKeyPressed(Keyboard.Key.Down) then player.move(Keyboard.Key.Down)
                       if Keyboard.IsKeyPressed(Keyboard.Key.Escape) then gameWin.Close()
                                    
                       gameWin.Clear()
                       if (player :> IActor).IsLive then
                                                    player.Update()
                                                               
                                    
                       slime.Update((float32)time)
                                   
                               
                       playercord.DisplayedString <- "player X: "+(player :> IActor).X.ToString()+" player Y: "+(player :> IActor).Y.ToString()
                                    //smilecord.DisplayedString <- "slime X: "+(slime :> IActor).X.ToString()+" slime Y: "+(slime :> IActor).Y.ToString()
                       statusString.DisplayedString <- player.StatusString
                                    //draw here!!!
                       for i = 0 to H - 1  do
                                           for j = 0 to W - 1  do 
                                                            if tileMap.[i].[j] = '#' then rect.FillColor <- SFML.Graphics.Color.White
                                                            if tileMap.[i].[j] = '-' then rect.FillColor <- SFML.Graphics.Color.Transparent
                                                            if tileMap.[i].[j] = 'T' then rect.FillColor <- SFML.Graphics.Color.Red
                                                            
                                                            rect.Position <- new Vector2f((float32)j * 32.0f - offsetX, (float32)i * 32.0f - offsetY)
                                                            gameWin.Draw(rect)
    
                       if not (player :> IActor).IsLive then MessageBox.Show("You Dead!", gameWin)
                       
                       gameWin.Draw((player :> IActor).Sprite)
                       gameWin.Draw(playercord)
                       gameWin.Draw((slime :> IActor).Sprite)
                       gameWin.Draw(statusString)
                       gameWin.Display()
                        
                       
                       match gameWin.IsOpen with 
                       | true -> mainLoop() |> ignore
                       | false -> ()
    mainLoop()
    0 // return an integer exit code
