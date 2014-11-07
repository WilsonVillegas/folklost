label start:
    jump startgame

label startgame:
    if talked_papa: 
        jump checkStatus

    $ talked_papa = False
    $ day_one_start = False
    $ day_two_start = False
    $ day_three_start = False
    $ day_one_finish = False
    $ day_two_finish = False
    $ day_three_finish = False
    $ talked_papa = True
    #play sound "angusgrunt"
    #$ checkvariable = False
    #play music "thememusic" fadein 5

    "Boys!" 
    "You know your old papa could fell a spruce with one swing?" 
    "Well, it ain't as easy when your legs get eaten by a drake!"
    "I reckon his head on my mantle was a fair trade."
    "But we've got to pay off our debts to the kingdom now."
    "I need you boys to hew in the woods in front of the house."
    "There's a tree that is read of leaf. Silverwood."
    "Find a Silverwood tree and we can pay off some of this debt before we get a visit from the royal guard."
    "Per, you're the oldest and strongest. Take the axe and find a Silverwood tree."

    $ day_one_start = True

    jump end

label checkStatus:
    if day_three_start: 
        jump MidDay3
    if day_two_finish: 
        jump StartDay3
    if day_two_start: 
        jump EndDay2
    if day_one_finish: 
        jump StartDay2
    if day_one_start: 
        jump EndDay1
    jump end

label EndDay1:
    "Son, your brother is out chopping wood."
    "What are you doing for the family? The fire needs to be tended before night falls."

    jump end

label StartDay2:

    "Trolls aren't so scary. If this was years ago I would have chopped him down as well."
    "An odd thing, trolls have a tendency to whistle in the forest. And they're good whistlers."
    "Well it seems like Per got spooked. Pål, you're next. Use that extra weight to fell a Silverwood."

    $ day_two_start = True

    jump end


label EndDay2:
    "I've got faith Pål will find a Silverwood. That boy can find mushrooms like a hog."
    "Don't forget to tend the hearth before nightfall."

    jump end

label StartDay3:

    "So the two biggest and brightest boys couldn't outdo the Troll."
    "I'm left with you, Espen Askeladd."
    "Can you get find this tree and avoid the troll?"

    menu:
        "I think I can do it.":
            jump StartDay3Yes
        "I'm not so sure.":
            jump StartDay3No

label StartDay3Yes:

    "Don't think. Do."
    "A son of mine is confident. You'll take down a drake someday."
    "Now take the axe and anything else you need before heading out to the forest."

    $ day_three_start = True

    jump end

label StartDay3No:

    "No? That won't cut it."
    "This is your chance to help our family."
    "Now, I normally wouldn't trust you with such a task, but we've run out of options."
    "Take the axe and anything else you need before heading out to the forest."

    $ day_three_start = True

    jump end

label MidDay3:

    "Take the axe and anything else you need before heading out to the forest."
    "Listen for the Troll's whistling."

    jump end


label end:
    return
