label start:
    $ talked_angus = False
    jump startgame

label startgame:
    if talked_angus: 
        jump checkStatus
    $ talked_angus = True
    #play sound "angusgrunt"
    #$ checkvariable = False
    #play music "thememusic" fadein 5

    "Boys!" 
    "You know your old pap could fell a spruce with one swing?" 
    "Well it ain't as easy when your legs get eaten by a drake!"
    "I reckon his head on my mantle was a fair trade."
    "But we've got to pay off our debts to the kingdom now."
    "I need you boys to hew in the woods in front of the house."
    "There's a tree that is read of leaf. Silverwood."
    "Find a Silverwood tree and we can pay off some of this debt before we get a visit from the royal guard."
    "Vlad, you're the oldest and strongest. Take the axe and find a Silverwood tree."


    jump end

label sentMe2:
    play sound "ChuckleSlow.wav"
    "And like a pawn you move where he commands, without a thought to the reason or overall plan."
    $ asked = True
    "Do you wonder at all what the reason might be that saw you wake up on a dock out at sea?"

    menu:
        "I have been asking. I've asked everyone on this godforsaken boat, and no one can give me a damn answer.":
            jump highPrice

label whatAreYou:
    play sound "Mmm.wav"
    "Do you not recognize the reflection, gleaming in the dark?"
    "An embodiment of all your sins, all your pain..."
    "All your guilt stares back at you from the abyss."

    menu:
        "What do you want from me then?":
            jump whatDoYouWant
        "The ferryman sent me to get your ticket.":
            jump sentMe

label whatDoYouWant:
    "The little girl, the drunken man, and the lover that lingers still."
    "A chain binds them all to me, and every link whispers a name." 
    "Marcus."

    menu:
        "What do you mean? I just met these people.":
            jump justMet

label end:
    return
