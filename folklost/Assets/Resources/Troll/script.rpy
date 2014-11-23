label startgame:

    "What do we have here? A little boy, far from home, cutting down my Silver Tree?"
    "I hope you weren't thinking of taking that wood home with you, boy. It's mine."

    menu:
        "I'm not scared of you! I cut it down so this wood is mine. Fair is fair."
            jump NotScared
        "Uh, um, wait! What if I traded you some nice cheese for the wood?"
            jump Cheese
        "Ahhhhhhhhhhhhhhhhhhh!!!"
            jump Scared

label NotScared:

    "You've got a lot to learn about the world. Like what a club to the head feels like."

    jump end

label Cheese:

    "Oh, that does look tasty. I'll take it. And the wood."

    jump end

label Scared:

    "Stop screaming already!"

    jump end

label end:
    $ cave = True
    return
