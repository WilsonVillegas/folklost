label startgame:
    $ dead = False

    "*I can't eat anymore!*"

    menu:
        "Well I’m starting to get full, but no matter. I’ll just cut my stomach open so I can eat more"
            jump Win
        "*cut a hole in the bowl so that porridge flows out*"
            jump CutBowl
        "I can’t do it! Too much porridge!"
            jump TooMuch

label Win:

    "That really works? There’s no way I’m going to lose to a puny child. Two can play at that game!"

    jump end

label CutBowl:

    "I can see that. You think you can trick me? How’s this for a trick?"

    jump DeadEnd

label TooMuch:

    "Ha, see? No one can best a troll at eating."

    jump DeadEnd

label end:
    return

label DeadEnd:
    $ dead = True
    return