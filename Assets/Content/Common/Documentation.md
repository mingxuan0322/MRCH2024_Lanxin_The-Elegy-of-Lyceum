# MRCH Template Documentation + Manual

Tips before we start

* Try to read QuickStart.md under the same folder before reading this.
* some technical information is mentioned in Scripts/Readme.md.

Where to find these components

​	Inspector=>Add Component=>MRCH-Interact ***or*** Inspector=>Add Component=>*Search*

### Interaction Trigger

#### 	Collider Trigger

* **Usage**: it can bring three events including ‘Trigger First Enter’, ‘Trigger Enter (each time)’, ‘Trigger Exit’, which are related to the Trigger component (a certain Collider with isTrigger on) on the same GameObject.
* **Requirement**: the GameObject that uses Interaction Trigger and having the ‘Use Collider Trigger’ on, has a **a certain type of Collider** and **having isTrigger on**.
* **Variables**: 
  * Use Collider Trigger (boolean): enable the functions
* **Functions**:
  * On Trigger First Enter: This event would be triggered when: the player enters the collider trigger only **for the first time**. The ‘first time’ flag would be cleared when reloading the scene or app.
  * On Trigger Enter: This event would be triggered when: the player enters the collider trigger each time.
  * On Trigger Exit: This event would be triggered when: the player exits the collider trigger each time.

#### 	Distance Trigger (not suggested)

* **Usage**: it can bring three events including ‘Distance First Enter’, ‘Distance Enter (each time)’, ‘Distance Exit’.
* **Requirement**: N/A.
* **Variables**: 
  * Use Distance Trigger (boolean): enable the functions
  * Distance: The distance that these events would count. Notably, we didn’t measure the scale ratio when scanning, so *I personally don’t suggest you to use it unless you test the ratio between the real world and the scanned cloud points yourself.* (XD)
* **Functions**:
  * On Distance First Enter: This event would be triggered when: the player enters the distance range only **for the first time**. The ‘first time’ flag would be cleared when reloading the scene or app.
  * On Distance Enter: This event would be triggered when: the player enters the distance range each time.
  * On Distance Exit: This event would be triggered when: the player exits the distance range each time.

#### 	LookAt Trigger

- **Usage**: it can bring three events including ‘Look At First Enter’, ‘Look At Enter (each time)’, ‘Look At Distance Exit’, which are related to the Trigger component (a certain Collider with isTrigger on) on the same GameObject.
- **Requirement**: the GameObject that uses Interaction Trigger and having the ‘Use Collider Trigger’ on, has a **a certain type of Collider** and **having isTrigger on**.
- **Variables**: 
  - Use Collider Trigger (boolean): enable the functions
- **Functions**:
  - On Trigger First Enter: This event would be triggered when: the player enter the collider trigger only **for the first time**. The ‘first time’ flag would be cleared when reloading the scene or app.
  - On Trigger Enter: This event would be triggered when: the player enters the collider trigger each time.
  - 