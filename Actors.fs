
module Actors 
open SFML.Graphics
open SFML.System
open SFML.Window
open Core
        type IActor = 
            abstract member DX:float32 with get, set
            abstract member DY:float32 with get, set
            abstract member X:float32 with get, set
            abstract member Y:float32 with get, set
            abstract member Sprite:Sprite with get, set
            abstract member IsLive:bool with get, set
            abstract member Hp:float32 with get, set
            abstract member MaxHp:float32 with get, set

        type Player() =
            let playerSprite = 
                                let s = new Sprite(Content.PlayerTexture)
                                s.TextureRect <- IntRect(1, 35, 40, 30)
                                s
            
            member val playerOnGround = true with get, set 
            
            interface IActor with 
                    member val DX = 0.0f with get, set
                    member val DY = 0.0f with get, set
                    member val X = 50.0f with get, set
                    member val Y = 150.0f with get, set
                    member val Hp = 100.0f with get, set
                    member val MaxHp = 100.0f with get, set
                    member val IsLive = true with get, set 
                    member val Sprite:Sprite = playerSprite with get, set
            
            member self.Level = 1
            
            member self.StatusString = 
                                    let lvl = "Level: "+self.Level.ToString()
                                    let hps = "Hp: "+(self :> IActor).Hp.ToString() + " / " + (self :> IActor).MaxHp.ToString()
                                    lvl+"\n"+hps

            member self.Collision(dir:int) = 
                                              for i = (int)((self :> IActor).Y / 32.0f) to (int)(( (self :> IActor).Y + 30.0f) / 32.0f) do 
                                                    for j = (int)((self :> IActor).X / 32.0f) to (int)(( (self :> IActor).X + 40.0f) / 32.0f) do 
                                                                                                                                           if Core.tileMap.[i].[j] = '#' then 
                                                                                                                                                                         if dir = 0 then 
                                                                                                                                                                                    if (self :> IActor).DX > 0.0f then (self :> IActor).X <- (float32)j * 32.0f - 32.0f
                                                                                                                                                                                    if (self :> IActor).DX < 0.0f then (self :> IActor).X <- (float32)j * 32.0f + 32.0f
                                                                                                                                                                         if dir = 1 then
                                                                                                                                                                                      //floor 
                                                                                                                                                                                     if (self :> IActor).DY > 0.0f then 
                                                                                                                                                                                                                   (self :> IActor).Y <- (float32)i * 32.0f - 32.0f
                                                                                                                                                                                                                   (self :> IActor).DY <- 0.0f
                                                                                                                                                                                                                   self.playerOnGround <- true
                                                                                                                                                                                     //potolok
                                                                                                                                                                                     if (self :> IActor).DY < 0.0f then 
                                                                                                                                                                                                                   (self :> IActor).Y <- (float32)i * 32.0f + 35.0f
                                                                                                                                                                                                                   (self :> IActor).DY <- 0.0f
            member self.Update() = 
                                  (self :> IActor).X <- (self :> IActor).X + (self :> IActor).DX
                                  
                                  self.Collision(0)
                                  
                                  if not self.playerOnGround then (self :> IActor).DY <- (self :> IActor).DY + 0.6f
                                  (self :> IActor).Y <- (self :> IActor).DY + (self :> IActor).Y
                                  self.playerOnGround <- false
                                  
                                  self.Collision(1)

                                  (self :> IActor).Sprite.Position <- new Vector2f((self :> IActor).X - offsetX, (self :> IActor).Y - offsetY)
                                  (self :> IActor).DX <- 0.0f

            member self.move(key) = 
                                   match key with 
                                    | Keyboard.Key.Left -> (self :> IActor).DX <- (self :> IActor).DX - 1.6f
                                    | Keyboard.Key.Right -> (self :> IActor).DX <- (self :> IActor).DX + 1.6f
                                    | Keyboard.Key.Up -> if self.playerOnGround then (self :> IActor).DY <- (self :> IActor).DY - 15.6f
                                                                                     self.playerOnGround <- false
                                    | Keyboard.Key.Down -> (self :> IActor).DY <- (self :> IActor).DY + 1.6f
