module Core 
    open SFML.Graphics
    open SFML.Graphics
    let H = 16
    let W = 36
    let tileMap:string[] = [|
                "####################################"
                "#----------------------------------#"
                "#----------------------------------#"
                "#----------------------------------#"
                "#----------------------------------#"
                "#----------------------------------#"
                "#----------------------------------#"
                "###################----------------#"
                "#----------------------------------#"
                "#----------------------------------#"
                "#---###----------------------------#"
                "#----------------------------------#"
                "#----------------------------------#"
                "#----------######------------------#"
                "#----------------------------------#"
                "####################################"
                           |]
    let mutable offsetX = 0.0f
    let mutable offsetY = 0.0f
    [<AbstractClass;Sealed>]
    type Content() = 
        static member private TEXTURE_DIR = @"./bin/Debug/Content/textures/"
        static member private FONT_DIR = @"./bin/Debug/Content/fonts/"

        static member val PlayerTexture:Texture = new Texture(Content.TEXTURE_DIR+"NPC_16.png")
        static member Font  = new Font(Content.FONT_DIR+"sansation.ttf")
        static member Load() = 
                               Content.PlayerTexture