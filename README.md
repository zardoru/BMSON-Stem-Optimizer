BMSON Stem Optimizer
===========================

Initially made for the Raindrop Arcade project.
A tool that optimizes single-use stems on your bmson files. Cuts out silence, short and not loud enough sections.
It's able to relocate notes for all bmson files that use your stems by using remap.json.
Optimization path doesn't need to be specified. Original files will not be overwritten so it's easier to work with them on the editor then convert to the 
optimized data.

Limitations
===========================
* Single-use stem only (continuation flag is false only once)
* Single BPM only (unless the stem start is aligned at y = 0)

