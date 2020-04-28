# MdotsCustomPlaymakerActions

This is a dump of my custom Playmaker actions I've made or modded for my own projects-
I hope they are useful to some other Playmakers out there ^_^

If you want to tip me for helping you make your own Fsm spaghetti
[paypal here](https://pledgie.com/campaigns/13952)
bitcoin == 17PUcU7tdhhghvwwximtigGGqvp6pcfUzk

I can't guarantee any support on these but if you have questions please post them in the
Playmaker forum as I'm always hanging out there [Playmaker forums](http://hutonggames.com/playmakerforum/index.php)

### If you want all the actions just download the "mdsPMActions.unitypackage"


## Add Knockback Force

![alt text](http://i.imgur.com/PftFmtB.png)

This applies a knockback force in the direction of the hit-
Use the hitpoint from a cast


## Animated UV Map

![alt text](http://i.imgur.com/WakgCww.png)

Animated the Offset on a material


## Are game objects facing each other

![alt text](http://i.imgur.com/ID1OMX5.png)

Determines whether two game objects are facing each other



## Array Clear Delete

![alt text](http://i.imgur.com/itBcVVQ.png)

Deletes all items from an array leaving it empty


## Array Remove

![alt text](http://i.imgur.com/08FvX1L.png)

Removes an object from an array


## Box Cast

![alt text](http://i.imgur.com/RJ0yxag.png)

Casts a physics box and retrieves hit info


## Button AutoScroll for GamePad

![alt text](http://i.imgur.com/mwpDFuP.png)

Use with a vertical list and the buttons will scroll with gamepad input


## Check Sphere

![alt text](http://i.imgur.com/DRwSfAF.png)

Casts a checksphere


## Debug Quaternion

![alt text](http://i.imgur.com/WFHBB9s.png)


## Enable Agent Action

![alt text](http://i.imgur.com/6pdY27Y.png)

Enables/Disables a navmesh agent component


## Enable Event System

![alt text](http://i.imgur.com/BQ1EgUx.png)

Enables/Disables a ugui event system


## Enable Mesh Renderer

![alt text](http://i.imgur.com/Ky5hVme.png)

Enables/Disables a mesh renderer


## Flip Fsm Bool

![alt text](http://i.imgur.com/aLPtcsm.png)

Flips a bool on another Fsm


## Float switch 2

![alt text](http://i.imgur.com/g874W9j.png)

Switches based on "equals"


## Front back side to side

![alt text](http://i.imgur.com/KgyXc4D.png)

Input a game object or V3 and it will output a V3 at a given direction from the target
-useful for getting flanking positions for ai agents


## Fsm Bool Test

![alt text](http://i.imgur.com/ld9wwQR.png)

Tests a bool on another fsm



## Game Object Speed no Y

![alt text](http://i.imgur.com/krz3ZGq.png)

Gets game object speed ignoring the Y velocity


## Get Box Collider Size

![alt text](http://i.imgur.com/8ILHYo7.png)


## Get Capsule Collider Height

![alt text](http://i.imgur.com/5x6SEWg.png)


## Get Collider Type

![alt text](http://i.imgur.com/9xtBGcs.png)


## Get forward direction

![alt text](http://i.imgur.com/NX1tGvZ.png)

Gets the forward +Z direction of a game object


## Get Light Intensity

![alt text](http://i.imgur.com/gf8SAAV.png)


## Get Light Range

![alt text](http://i.imgur.com/perhiss.png)


## Get Sphere Collider Radius

![alt text](http://i.imgur.com/vvX7ZLl.png)


## Get Surface Forward

![alt text](http://i.imgur.com/z6hDknR.png)

Input vars from a cast hit and use the surface fwd to align things to surface


## Get Y Distance

![alt text](http://i.imgur.com/z2Ocaoi.png)


## Int Remap

![alt text](http://i.imgur.com/ocVUnlr.png)

Same as float remap except with an int var


## Inverse Transform Vector

![alt text](http://i.imgur.com/WwIsJdD.png)


## Is Navmesh Component enabled

![alt text](http://i.imgur.com/FYVO7FP.png)


## Is Target In Front or Behind

![alt text](http://i.imgur.com/Z1YlKpv.png)


## Knockback Action

![alt text](http://i.imgur.com/4QFnt15.png)

Give it a hitpoint from a cast and an amount and it will knockback a NavmeshAgent, 
RigidBody or Character Controller


## Knockback Position Finder

![alt text](http://i.imgur.com/NltBzfI.png)

Give it cast hit info and it will give you a knockback position


## OverlapSphere To Array

![alt text](http://i.imgur.com/xC3fmwD.png)

Casts an overlapSphere and stores hit game objects in an array


## Physics LineCast 2

![alt text](http://i.imgur.com/G9EaUu0.png)



## Play Single Audio Random Pitch

![alt text](http://i.imgur.com/oi3INei.png)

Plays a single audio file while randomizing pitch each time


## Play Random Audio From Array

![alt text](http://i.imgur.com/BU3yQ3B.png)

Plays a random audio file while from an array while randomizing pitch each time
(make sure you use an "object" var set to UnityEngine/AudioClip for the variable)



## Raycast Ignore Triggers

![alt text](http://i.imgur.com/KnCemhk.png)

Same as normal raycast but has option to ignore colliders set to trigger


## Raycast 2 Ignore Triggers

![alt text](http://i.imgur.com/bO8THTO.png)

Same as normal raycast2 but has option to ignore colliders set to trigger


## Rotate Around Target

![alt text](http://i.imgur.com/o02Uf21.png)

You can use this to do DarkSouls like targeting when combined with
"Smooth Follow Action No Look At" and a LookAt pointing at your target


## Rotate to Forward Direction

![alt text](http://i.imgur.com/YY0yQSv.png)

Feed this a hitNormal from a cast to align objects to the hit object surface


## Rotate to Up Direction

![alt text](http://i.imgur.com/n6BGEAd.png)

Feed this a hitNormal from a cast to align objects to the hit object surface


## Simple Overlap Box

![alt text](http://i.imgur.com/2APjb9P.png)

Casts an OverlapBox and returns the first object hit
(can be used as an alternative to box colliders for melee collisions)


## Simple Overlap Capsule Plus

![alt text](http://i.imgur.com/30jmd5O.png)

Casts OverlapCapsule returns hitobject and hitpoint-
* works will all collider types except mesh and terrain


## Simple Overlap Sphere

![alt text](http://i.imgur.com/5YbTiFa.png)

Casts an OverlapBox and returns the first object hit


## Simple Overlap Sphere Plus

![alt text](http://i.imgur.com/6JgIqm3.png)

Casts OverlapSphere returns hitobject and hitpoint-
* works will all collider types except mesh and terrain


## Smooth Follow Action No Look At

![alt text](http://i.imgur.com/uwyu8We.png)

A smooth follow action that does not look at the object its following


## Sphere Cast 2

![alt text](http://i.imgur.com/b7Bs7Zc.png)


## Subtract Fsm Int

![alt text](http://i.imgur.com/tQXKZZV.png)


## Transform Vector

![alt text](http://i.imgur.com/Nlag1Ip.png)


## Trigger Event Ignore Game Object

![alt text](http://i.imgur.com/r1mvnJc.png)

Works same as Trigger event except will ignore collisions with a game object var


## Trigger Event Once Per Object

![alt text](http://i.imgur.com/7QepfoA.png)

Works same as Trigger event except will only allow objects to trigger once each


## Trigger Event Store Hit Object

![alt text](http://i.imgur.com/9yzcmk3.png)

Works same as Trigger event except stores hit object as gameObject instead of collider


## Ugui Set First Selected Game Object

![alt text](http://i.imgur.com/nsD1cUK.png)

## Flight Path Action

![alt text](http://i.imgur.com/h49jpN6.png)

Does basic flight pathfinding with a rigid body using Raycasts- example flight
![alt text](https://fat.gfycat.com/UnknownBitterGuineapig.gif)
Rigid Body settings
![alt text](http://i.imgur.com/3mtpsdy.png)
















































