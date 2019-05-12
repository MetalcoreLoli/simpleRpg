
module Actors 
open SFML.Graphics
open SFML.System
open SFML.Window
open Core
open System

        type IActor = 
            abstract member DX:float32 with get, set
            abstract member DY:float32 with get, set
            abstract member X:float32 with get, set
            abstract member Y:float32 with get, set
            abstract member Sprite:Sprite with get, set
            abstract member IsLive:bool with get, set
            abstract member Hp:float32 with get, set
            abstract member MaxHp:float32 with get, set

        type Slime() = 
            let slimeSprite = 
                              let sprite = new Sprite(Content.SlimeTexture)
                              sprite.TextureRect <- IntRect(1, 35, 40, 30)
                              sprite.Color <- SFML.Graphics.Color.Green
                              sprite

            member val slimeOnGround = true with get, set 
            interface IActor with 
                    member val DX = 0.05f with get, set
                    member val DY = 0.0f with get, set
                    member val X = 150.0f with get, set
                    member val Y = 50.0f with get, set
                    member val Hp = 100.0f with get, set
                    member val MaxHp = 100.0f with get, set
                    member val IsLive = true with get, set 
                    member val Sprite:Sprite = slimeSprite with get, set
            member self.Collision() = 
                                              for i = (int)((self :> IActor).Y / 32.0f) to (int)(( (self :> IActor).Y + 30.0f) / 32.0f) do 
                                                    for j = (int)((self :> IActor).X / 32.0f) to (int)(( (self :> IActor).X + 40.0f) / 32.0f) do 
                                                                                                                                           if Core.tileMap.[i].[j] = '#' then 
                                                                                                                                                                    
                                                                                                                                                                            if (self :> IActor).DX > 0.0f then 
                                                                                                                                                                                                        (self :> IActor).X <- (float32)j * 32.0f - 40.5f
                                                                                                                                                                                                        (self :> IActor).DX <- (self :> IActor).DX * -1.0f
                                                                                                                                                                            if (self :> IActor).DX < 0.0f then 
                                                                                                                                                                                                        (self :> IActor).X <- (float32)j * 32.0f + 32.0f
                                                                                                                                                                                                        (self :> IActor).DX <- (self :> IActor).DX * -1.0f

                                                                                                                                           if Core.tileMap.[i].[j] = 'T' then 
                                                                                                                                                if(self :> IActor).DY > 0.0f then 
                                                                                                                                                                             if (self :> IActor).Hp > 0.0f then (self :> IActor).Hp <- (self :> IActor).Hp - 0.1f
                                                                                                                                                                             if (self :> IActor).Hp < 0.0f then (self :> IActor).IsLive <- false
            member self.move(dir) = 
                               if dir = 1 
                               && Core.tileMap.[(int)(((self :> IActor).Y) / 32.0f)].[(int)(((self :> IActor).X) / 32.0f)] <> '#' 
                               then (self :> IActor).X <-  (self :> IActor).X + (self :>IActor).DX + 1.5f 
                                    
                               
                               if dir = 0 
                               && Core.tileMap.[(int)((self :> IActor).Y / 32.0f)].[(int)(((self :> IActor).X - 1.5f) / 32.0f)] <> '#' 
                               then (self :> IActor).X <-  (self :> IActor).X + (self :>IActor).DX - 1.5f
                           
                             

           
            member self.Update(time:float32) = 
                                  (self :> IActor).X <- (self :> IActor).X + (self :> IActor).DX * time

                                  self.Collision()
                                  
                                  if not self.slimeOnGround then (self :> IActor).DY <- (self :> IActor).DY + 0.6f
                                  (self :> IActor).Y <-  (self :> IActor).Y + (self :> IActor).DY 
                                  //self.slimeOnGround <- false
                                  
                                  if not (self :> IActor).IsLive then (self :> IActor).Sprite.Color <- SFML.Graphics.Color.Red
                                  //self.Collision(0)
                                  (self :> IActor).Sprite.Position <- new Vector2f((self :> IActor).X - offsetX, (self :> IActor).Y - offsetY)
                                  //(self :> IActor).DX <- 0.0f
        
        //player
        type Player() =
            let playerSprite = 
                                let s = new Sprite(Content.SlimeTexture)
                                s.TextureRect <- IntRect(1, 35, 40, 30)
                                s.Color <- SFML.Graphics.Color.Yellow
                                s
          
            member val playerOnGround = true with get, set 
            
            interface IActor with 
                    member val DX = 0.0f with get, set
                    member val DY = 0.0f with get, set
                    member val X = 50.0f with get, set
                    member val Y = 250.0f with get, set
                    member val Hp = 100.0f with get, set
                    member val MaxHp = 100.0f with get, set
                    member val IsLive = true with get, set 
                    member val Sprite:Sprite = playerSprite with get, set
            
            member self.Level = 1
            member self.Intersects(actor:IActor):bool = (self :> IActor).X / 32.0f > (actor :> IActor).X / 32.0f && (self :> IActor).X / 32.0f < (actor :> IActor).X / 32.0f
                                                     || (self :> IActor).Y / 32.0f > (actor :> IActor).Y / 32.0f && (self :> IActor).Y / 32.0f < (actor :> IActor).Y / 32.0f 

            member self.StatusString = 
                                    let lvl = "Level: "+self.Level.ToString()
                                    let mutable hp = (int)(self :> IActor).Hp
                                    let mutable maxhp = (int)(self :> IActor).MaxHp
                                    let hps:string = "Lvl: "+self.Level.ToString()+"\nHp: "+hp.ToString()+" / "+maxhp.ToString()
                                    hps+"\n"

            member self.Collision(dir:int) = 
                                              for i = (int)((self :> IActor).Y / 32.0f) to (int)(( (self :> IActor).Y + 30.0f) / 32.0f) do 
                                                    for j = (int)((self :> IActor).X / 32.0f) to (int)(( (self :> IActor).X + 40.0f) / 32.0f) do 
                                                                                                                                            if Core.tileMap.[i].[j] = '#' then 
                                                                                                                                                                         if dir = 0 then 
                                                                                                                                                                                    if (self :> IActor).DX > 0.0f then (self :> IActor).X <- (float32)j * 32.0f - 40.5f
                                                                                                                                                                                    if (self :> IActor).DX < 0.0f then (self :> IActor).X <- (float32)j * 32.0f + 32.0f
                                                                                                                                                                         if dir = 1 then
                                                                                                                                                                                      //floor 
                                                                                                                                                                                     if (self :> IActor).DY > 0.0f then 
                                                                                                                                                                                                                   (self :> IActor).Y <- (float32)i * 32.0f - 30.5f
                                                                                                                                                                                                                   (self :> IActor).DY <- 0.0f
                                                                                                                                                                                                                   self.playerOnGround <- true
                                                                                                                                                                                     //potolok
                                                                                                                                                                                     if (self :> IActor).DY < 0.0f then 
                                                                                                                                                                                                                   (self :> IActor).Y <- (float32)i * 32.0f + 32.5f
                                                                                                                                                                                                                   (self :> IActor).DY <- 0.0f
                                                                                                                                            if Core.tileMap.[i].[j] = 'P' then 
                                                                                                                                                                                Core.level <- level - 1
                                                                                                                                                                                Core.tileMap <- Core.levels.[level]
                                                                                                                                            if Core.tileMap.[i].[j] = 'N' then 
                                                                                                                                                                                Core.level <- level + 1
                                                                                                                                                                                Core.tileMap <- Core.levels.[level]

                                                                                                                                            if Core.tileMap.[i].[j] = 'T' then 
                                                                                                                                                                             if(self :> IActor).DY > 0.0f then 
                                                                                                                                                                                                          if (self :> IActor).Hp > 0.0f then (self :> IActor).Hp <- (self :> IActor).Hp - 1.0f
                                                                                                                                                                                                          if (self :> IActor).Hp < 0.0f then (self :> IActor).IsLive <- false
                                                                                                                                                                                                         //
            member self.Update(time) = 
                                  (self :> IActor).X <- (self :> IActor).X + (self :> IActor).DX * time
                                  
                                  self.Collision(0)
                                  
                                  if not self.playerOnGround then (self :> IActor).DY <- (self :> IActor).DY + 0.6f
                                  (self :> IActor).Y <-  (self :> IActor).Y+ (self :> IActor).DY 
                                  self.playerOnGround <- false
                                  
                                  self.Collision(1)
                                  
                                  (self :> IActor).Sprite.Position <- new Vector2f((self :> IActor).X - offsetX, (self :> IActor).Y - offsetY)
                                  (self :> IActor).DX <- 0.0f

            member self.move(key) = 
                                   match key with 
                                    | Keyboard.Key.Left -> (self :> IActor).DX <- (self :> IActor).DX - 0.5f
                                    | Keyboard.Key.Right -> (self :> IActor).DX <- (self :> IActor).DX + 0.5f 
                                    | Keyboard.Key.Up -> if self.playerOnGround then (self :> IActor).DY <- (self :> IActor).DY - 15.6f
                                                                                     self.playerOnGround <- false
                                    | Keyboard.Key.Down -> (self :> IActor).DY <- (self :> IActor).DY + 0.6f
