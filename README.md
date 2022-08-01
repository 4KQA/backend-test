# AKQA Backend Task for National Earthquake Research Department

## Problem Description

An earthquake has split the planet Earth into two separate hemispheres at the equatorial line and N.E.R.D.(National Earthquake Research Department) needs your help to make a register of survivors.

## Requirements

You will develop a ***REST API***, which will store information about the people either reporting in or deceased individuals, found by survivors and a simple interface to report individuals and get reports.

To accomplish this, the API must fulfill the following use cases:

- **Add people to the "database" (in memory is fully acceptable - no need to set up databases!)**

  A person must have a *firstname*, *lastname*, *age*, *gender*, *last location (latitude, longitude)* and *a flag to mark if they're alive*.

- **Update survivor location**

  A survivor must have the ability to update their last location, storing the new latitude/longitude pair in the base (no need to track locations, just replacing the previous one is enough).
  
  The updated location must be on the same hemisphere (north or south), since the globe is splitted and traveling is not yet possible, but communication is!

- **Reports**

  The API must offer the following reports:

    1. Percentage of survivors.
	2. List of survivors, including their last known location
	3. Search on lastname in order to find or track down relatives and check if they're deceased

---------------------------------------

## Notes

1. No authentication is needed
2. Architecture and programming techniques are still important in this chaos!
3. Documentation of the client facing services are required so other vendors can make UI's!
4. It's up to you to decide how you will create your solution and how much user friendliness you will put into this system, the optimal solution is the one that is possible to use, but still making the system available for N.E.R.D as fast as possible, still showcasing your programming skills!
5. Use commits to split your changes into little pieces and use clear commit messages
6. If you have a database available, you can use that. If not, don't use time on setting that up. How you store individuals will not affect the evaluation!
7. How to implement the search etc. is up to you. 
8. The looks of the user interface will not be part of the evaluation, but the features will.
9. If some info about latitude/longitude is needed, check out https://www.bbc.co.uk/bitesize/topics/zvsfr82/articles/zd4rmfr

## Q&A

> How to submit my result?

Fork the repo and make a pull request. Deadline will be noted directly to you.

> What if I have a question?

Create a new issue or write an email to nicklas.dyrvig@akqa.com
