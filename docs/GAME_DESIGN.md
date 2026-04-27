# Game Design: Airport Survival

## One Sentence
Work through stressful airport day shifts and dangerous night patrols, earn enough mastery to surpass the tyrannical manager, and take over the airport.

## Genre
- Unity 3D or 2.5D survival life sim.
- Daytime task management.
- Nighttime stealth/security horror.
- Progression-driven job mastery.

## Tone
Stressful but funny. The manager should feel annoying, dramatic, and memorable rather than purely realistic. Night should feel darker and more threatening, with thieves as active targets and ghosts as hazards.

## Player Goal
Max out every job category, minimize scold count, keep stamina under control, earn money and points, then defeat or surpass the manager to take ownership of the airport.

## Core Loop
1. Wake up in the airport.
2. Choose a main daytime job: restaurant, check-in, or information desk.
3. Perform the selected job while also cleaning trash whenever it appears.
4. Avoid mistakes and backlog so the manager does not appear.
5. Earn money, stamina recovery opportunities, and job points.
6. At night, patrol the airport.
7. Catch thieves while avoiding ghosts.
8. End the day, apply job point gains and inactivity decay.
9. Repeat until all job categories reach max level.

## Main Characters

### Player
- Low-status airport worker.
- Can work, clean, guide passengers, process check-ins, serve food, and patrol at night.
- Has stamina.
- Loses stamina when scolded by the manager.
- Gains money and job points by completing work.
- Wins by mastering all work types and taking over the airport.

### Manager
- Main villain and pressure mechanic.
- Appears quickly when the player neglects tasks or makes serious mistakes.
- Scolds the player with escalating dialogue.
- Every scolding reduces stamina.
- Every scolding must mention the current total scold count.
- High-skill play means clearing the game with the lowest scold count possible.

Example scold lines:
- "Again? This is scolding number {count}. Do your job properly."
- "I knew I would find you like this. That makes {count} times."
- "You want this airport? Survive one day without making me shout first. Count: {count}."

## Daytime Jobs

### Cleaning
- Always active, regardless of selected main job.
- Trash appears around the airport during the day.
- If trash remains too long, the manager appears and scolds the player.
- Cleaning gives cleaning points and small money rewards.

### Restaurant
- Main job option.
- Tasks include dishwashing, serving, and clearing tables.
- If orders, dishes, or tables pile up, the manager scolds the player.
- Gives restaurant points.

### Check-In
- Main job option.
- Help travelers complete check-in.
- Must detect suspicious or criminal passengers.
- If the player processes a criminal, the manager scolds the player.
- Gives check-in points.

### Information Desk
- Main job option.
- Guide travelers to gates, services, baggage claim, or restaurants.
- Wrong directions trigger complaints and manager scolding.
- Gives guidance points.

## Night Patrol
- The airport becomes dark.
- The player must patrol marked zones.
- Thieves can infiltrate and should be caught.
- Ghosts can appear but cannot be caught.
- Ghosts must be avoided.
- Night gives security points.
- Failing to catch thieves or getting hit by ghosts can cost stamina and trigger penalties.

## Progression
Each job has points and levels:
- Cleaning
- Restaurant
- Check-In
- Guidance
- Security

All categories must reach max level to unlock the final confrontation with the manager.

## Inactivity Decay
If the player repeats the same job and ignores other job categories, ignored categories slowly lose points.

Rule draft:
- Each category tracks `daysSincePracticed`.
- If `daysSincePracticed` is 2 or more, that category loses points at day end.
- The longer it is ignored, the larger the point loss.
- Cleaning and security count separately because cleaning is daytime always-on and security is nighttime.

This forces players to distribute work instead of grinding only one job.

## Resources
- Stamina: lowered by scolding, ghosts, overwork, and failed events.
- Money: earned through work, used for recovery or upgrades.
- Job points: unlock levels.
- Scold count: score penalty and comedic pressure metric.

## Win Condition
Reach max level in all job categories, unlock the final airport takeover event, and beat the manager challenge.

## Skill Expression
The best player:
- Gets scolded rarely.
- Balances job categories to avoid decay.
- Cleans opportunistically.
- Identifies criminals during check-in.
- Gives correct directions.
- Catches thieves efficiently.
- Avoids ghosts at night.
