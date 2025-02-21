# LD45 - Top Seed!

This was my entry to the Ludum Dare 45 game jam, Made iin 72 hours! Inspired by a combination of Super Monkey Ball and Katamari Damaci, I attempted to mash the two games together and see hoow their mechanics could play off eachother.

A quick retrospective on things I liked and didn't like about this project:

I enjoyed the mashup of genres, and I feel like I barely touched the surface of what is possible here. Your access to areas of the level depends on the size of your ball, creating a sense of planning and progression as you figure out how to approach the levels. I feel like I could explore this further by creating larger levels with more pathways... Kinda like Katamari, haha!
But unlike Katamari, the goal is not to get as large as possible, but rather to reach the end of the level. This makes you have to actually choose weather picking up items is a good thing or a bad thing, cause it depends on the context of the level. I demonstrated this in one level where picking up too many fruits would cause you to not fit through the path to the exit. This could be expanded on.

It turns out, 3D stuff is pretty challenging, especially under a strict time limit. Any tools you can get to simplify the process of level creation (like Probuilder) is highly valuable in a game jam like this.

I should have approached movement differently. Right now, there is a lot of complicated math that actually tilts the entire stage underneath the player. While it mostly works, there is a lot of side effect problems that I could never address:
Since the ball is always rolliing downhill, every time the stage tilts it moves downwards. If it moves low enough, the stage can actually fall past the kill barrier, making it possible for the player to lose by touching the kill barrier even if they didn't fall off the course. It also caused a lot of issues with physics of moving platforms not working as expected since the rigidbodies are being moved around by a parent.
The code for tilting the stage was also just overly-coomplicated and not maintainable. How I should have handled it in retrospect was to keep the stage static and simply change the direction of gravity. I could also sell the illusion of the stage "tilting" by changing the angle of the light source, skybox, and camera perspective.

Even with all its flaws, I really enjoyed working on this one.
