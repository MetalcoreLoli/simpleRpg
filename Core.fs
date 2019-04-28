module Core 
    open SFML.Graphics
    open SFML.Graphics
    let H = 16
    let W = 72
    let mutable tileMap:string[] = [|
                "########################################################################"
                "#----------------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#######----------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#---###----------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#----------------------------------------------------------------------#"
                "#----------######------------------------------------------------------#"
                "#-----------------T-------------T-----------T--------------------------#"
                "########################################################################"
                           |]
    let mutable offsetX = 0.0f
    let mutable offsetY = 0.0f
    [<AbstractClass;Sealed>]
    type Content() = 
        static member private TEXTURE_DIR = @"Content\textures\"
        static member private FONT_DIR = @"Content\fonts\"

        static member val SlimeTexture:Texture = new Texture(Content.TEXTURE_DIR+"NPC_16.png")
        static member Font  = new Font(Content.FONT_DIR+"sansation.ttf")
        static member Load() = 
                               Content.SlimeTexture
                               Content.Font
