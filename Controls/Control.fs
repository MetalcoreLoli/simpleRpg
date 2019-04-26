namespace Control
module Control =
    open System
    open SFML
    open SFML.System
    open SFML.Graphics

    type Control() as self =
         member val X = 0.0f with get, set
         member val Y = 0.0f with get, set
         member val Width = 0.0f with get, set
         member val Height = 0.0f with get, set
         member val Text = "" with get, set

    type Button() as self= 
            inherit Control() 
            let rect = new RectangleShape()
            let text = new Text(self.Text, Core.Content.Font)
            member self.ButtonDrawable = 
                                        rect.Position <- new Vector2f(self.X, self.Y)
                                        rect.Size <- new Vector2f(self.Width, self.Height)
                                        rect.FillColor <- SFML.Graphics.Color.Red
                                        rect.OutlineColor <- SFML.Graphics.Color.Blue
                                        rect.OutlineThickness <- 5.0f
                                        rect