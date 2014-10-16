label startgame:
    
    if talked:
        jump checkstatus
    
    $ talked = True
    
    "Hello."
    "Where did you come from?"

    menu:
        "I can't remember.":
            jump nobody
        "I remember the ocean...":
            jump ocean
        "I was kidnapped.":
            jump kidnapped

label checkstatus:
    if finished_talk
        return
    if named:
        jump finishedtalk
    jump teds2

label start:
    $ talked = False
    $ named = False

label nobody:
    "Hmm..."
    "Your story is clouded in mystery."
    
    menu:
        "Thanks":
            jump finishedtalk
        "You're telling me...":
            jump finishedtalk

label ocean:
    "Hmm..."
    "Your story is of ships and treasure."
    
    menu:
        "Thanks":
            jump finishedtalk
        "When do I get rich?":
            jump finishedtalk

label kidnapped:
    "Hmm..."
    "Your story is of loss and discovery."
    
    menu:
        "Thanks":
            jump finishedtalk
        "Where am I now?":
            jump finishedtalk

label finishedtalk:
    "That is all I can say for now."
    "The rest has yet to be written."
    jump end


label end:
    $ named = True
    return
