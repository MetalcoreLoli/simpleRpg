module Events
    open System
   
    type MouseClickEvent() = 
        let ClickEvent = new Event<_>()

        [<CLIEvent>]
        member self.Click = ClickEvent.Publish

        member self.EventClick(arg) = ClickEvent.Trigger(self, arg)


