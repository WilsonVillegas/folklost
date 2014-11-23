label start:
    $ talked_bird = False
    jump startgame

label startgame:
    if talked_bird:
        jump Fly
    $ talked_bird = True

    "It's a little late to be wandering around at night." 
    "What is your name, child?"

    menu:
        "I don't know."
            jump NoRemember
        "I can't remembner..."
            jump Espen

    jump end

label NoRemember:

    "I can see it in your face now, you are Espen Askeladd."
    "You're family needs to pay off debts. I'm sure that is why you are in the forest."
    "To find and cut down a Silver Wood Tree. Its wood is valued beyond compare."
    "But even that treasure comes at a price."
    "These woods belong to a Troll. If he finds you, he will catch you and eat you."
    "If you hear his whistling, he has seen you."
    "Heed my warning, Espen Askeladd."
    jump end

label Espen:

    "It's important to know who you are, Espen Askeladd."
    "You're family needs to pay off debts. I'm sure that is why you are in the forest."
    "To find and cut down a Silver Wood Tree. Its wood is valued beyond compare."
    "But even that treasure comes at a price."
    "These woods belong to a Troll. If he finds you, he will catch you and eat you."
    "If you hear his whistling, he has seen you."
    "Heed my warning, Espen Askeladd."
    jump end

label Fly:

    "You must find your own way in the forest."
    "Go."
    jump LastEnd


label LastEnd:
    $ BirdAgain = True
    return

label end:
    $ BirdAgain = True
    return
