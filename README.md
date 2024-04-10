# CS490 Capstone Project: VR Orbit
An educational VR demo focusing on Earth's orbit and Kepler's Laws of Planetary Motion. This project was written in C# and developed in Unity. 

## Main features
- Mathematically correct orbits
  - Berger's Solution (1978) implemented to calculate Milankovitch paramters
    - Function of time (specific to a calendar date)
    - outputs eccentricity, obliquity, and longitude of perihelion
- Forward and Inverse Keplerian functions to determine Earth's position based on time and vice versa
- Seasonal day-length calculation
- Sun Declination and Solar Insolation (WIP)

insert images and gifs

## Installation

somethingsomething unity steam vr 

## Documentation
This section contains a brief overview of the scripts written in this project.

### bergers.cs

This file uses Berger's solution to calculate the three Milankovitch parameters as a function of YEAR. 

Berger's solution utilizes three different arrays (tables from Berger) containing obliquity, eccentricity, and precession values based on the year provided.

Obliquity and precession values are returned in degrees, and eccentricity is returned as a double (dimensionless).

### keplerian.cs

This file contains functions solving the forward and inverse Keplerian functions. 

The forward Keplerian function returns the time since the last perihelion passage and mean anomaly.

The inverse Keplerian function returns the true anomaly.

### seasons.cs

Calculates the day lengths of winter, spring, summer, and fall seasons of the year.

Utilizes the forward Keplerian function to find the day lengths of each season, with precession determining the formulas.

### generate_rot.cs

Helper function utilized in orbit.cs to find radius vectors

### orbit.cs

This file utilizes the previous files to generate orbital data. OrbitCalc returns solar declination, solar insolation, season day lengths, radius vector length from the sun to Earth, hour length of the day, and true anomaly.


# FAQ


## Notice
This project is in collaboration with Dr. Kostadinov, utilizing his MATLAB code and research and translating it into C# for the purpose of this project.

This project was developed by Michael Andrews, William Phong, Dennis Lai, Christian Contreras, and Hugo Bulnes. 


## Links
Earth Orbit Research Paper: https://gmd.copernicus.org/articles/7/1051/2014/

Earth Orbit Model Source Code: https://zenodo.org/records/4346609
