# TIRPClo – A Complete and Efficient Algorithm for Frequent Closed Time Intervals-Related Patterns Mining

## Introduction
Welcome! 

This repository is related to the paper *"Complete Closed Time Intervals-Related Patterns Mining"*, *AAAI 2021*. 

In the paper we introduce the *TIRPClo* algorithm, which we are extremely happy to make available online.

TIRPClo is a time intervals mining algorithm that efficiently and completely discovers the set of frequent closed TIRPs.

We are highly confident that this repository's contents will contribute to future research in the field of time intervals mining.

## Repository Contents
The contents of this repository are as follows: 
1. The complete implementation of the TIRPClo algorithm, implemented in Visual C#.
2. Various real-world time intervals datasets on which the algorithm can be run.

## Datasets
1.	***Location***: All the datasets are available under the *TIRPClo/TIRPClo/bin/Debug/netcoreapp2.1/Datasets/* directory 
2.	***Contents***: 
    1. ASL (Papapetrou et al. 2009)
    2. Two versions of the diabetes dataset (Moskovitch and Shahar 2015) 
    3. Smart-home (Jakkula and Cook 2011)
3. ***Format***: The datasets are in a *.csv* format, which contains the following information
    1. Total number of entities' STIs series in the dataset.
    2. Two rows for each entity:
        1. The first row contains the entity ID
        2. The second row contains the entity's ordered symbolic time intervals series, in which each symbolic time interval is represented as a triplet of a start-time, a finish-time and a symbol; separated by commas. Successive symbolic time intervals are separated by a semicolon.
4. ***Description***: For each dataset, a *{dataset}.txt* file is supplied, which lists the dataset's main properties.

## Dependencies
1. Visual Studio 2015/2017/2019.
2. .NET Core 2.1 Framework.

## Running Instructions
The recommended way to execute each of the algorithms on a selected dataset is as follows:
1. Download the repository and install the dependencies.
2. Open the desired algorithm's project using Visual Studio.
3. Right-click the project directory in the solution explorer > Properties > Select ".NET Core 2.1" as the Target Framework > Save. 
4. Open the *RunAlgorithm.cs* file and set the following algorithm's execution parameters within the main function to the desired values 
(default values are supplied, see [Example](#Example))
    1. Number of entities in the dataset
    2. Minimum vertical support percentage
    3. Maximal gap value
    4. Path to the dataset (without the *".csv"* suffix)
5. Run the project.

## Output
As soon as the algorithm terminates, the two following files are created within the same directory as the input dataset 
- The primary output file: *"{dataset name}-support-{minimum vertical support}-maxgap-{maximal gap}.txt"* – 
which stores the complete set of the discovered frequent TIRPs, including their features (e.g., vertical support) and their supporting instances.
- Another file, named *"{primary output file name}-stats.txt"*, which contains the total execution duration in milliseconds.

## Example
The following default parameters values are supplied within the *RunAlgorithm.cs* file, which is available under the algorithm's project directory:
- *number of entities*=65.
- *minimum vertical support*=50%.
- *maximal gap*=50.
- *file path*='Datasets/ASL/ASL'.

Running the TIRPClo algorithm with this set of parameters values results in the discovered frequent closed TIRPs file 
*"ASL-support-50-maxgap-50.txt"* as well as the *"ASL-support-50-maxgap-50.txt-stats.txt"* file, 
which contains the total time duration of the algorithm's execution. 
These files are created under the *TIRPClo/TIRPClo/bin/Debug/netcoreapp2.1/Datasets/ASL/* directory.

## References
[1]	Jakkula, V., & Cook, D. (2011, August). Detecting anomalous sensor events in smart home data for enhancing the living experience. In Workshops at the twenty-fifth AAAI conference on artificial intelligence.

[2]	Moskovitch, R., & Shahar, Y. (2015c). Fast time intervals mining using the transitivity of temporal relations. Knowledge and Information Systems, 42(1), 21–48.

[3]	Papapetrou, P., Kollios, G., Sclaroff, S., & Gunopulos, D. (2009). Mining frequent arrangements of temporal intervals. Knowledge and Information Systems, 21(2), 133.
