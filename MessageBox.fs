module MessageBox 
    open SFML
    open SFML.Graphics
    open SFML.Graphics
    open SFML.Graphics
    open SFML.System
    open Core
    open SFML.Graphics
    open SFML.Graphics
    open SFML.System
    open SFML.System

    [<AbstractClass;Sealed>]
    type MessageBox() = 
        
        static member private H = 400.0f
        static member private W = 300.0f
        
        static member Show(message:string, panel:RenderWindow):unit = 
                                                let rect = new RectangleShape()
                                                rect.Position <- new Vector2f(MessageBox.H, MessageBox.W)
                                                rect.FillColor <- SFML.Graphics.Color.White
                                                rect.OutlineColor <- SFML.Graphics.Color.Cyan
                                                rect.OutlineThickness <- 5.0f
                                                rect.Size <- new Vector2f(250.0f, 150.0f)
                                                
                                                let messageText = new Text(message, Content.Font)
                                                messageText.FillColor <- SFML.Graphics.Color.Black
                                                messageText.DisplayedString <- message
                                                messageText.Position <- new Vector2f(MessageBox.H, MessageBox.W)
                                                
                                                let button = new Control.Control.Button(
                                                                                        Text = "ClickMe!", 
                                                                                        Width = 50.0f, 
                                                                                        Height = 30.0f, 
                                                                                        X = MessageBox.H, 
                                                                                        Y = MessageBox.W + 85.5f)
                                                panel.Draw(rect)
                                                panel.Draw(button.ButtonDrawable)
                                                panel.Draw(button.ButtonText)
                                                panel.Draw(messageText)

                                                

