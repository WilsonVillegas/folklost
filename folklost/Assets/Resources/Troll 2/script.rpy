label startgame:

    "Dinner time little boy."
    "You're lucky."
    "I’m feeling nice today, so I’m gonna give you a chance to escape. A porridge eating contest."
    "Hope you’re hungry."

    menu:
        "I don't think I could eat that much porridge if I had the rest of my life!."
            jump RestLife
        "I'm a growing boy from a poor family. Of course I'm hungry. Just watch!"
            jump JustWatch

label RestLife:

    "Then you'll lose. And you'll be my dessert! Hahaha."

    jump end

label JustWatch:

    "I like your gusto kid. You’re brave, but stupid. Hahaha."

    jump end

label end:
    return
