#include "stdafx.h"
#include <iostream>
#include <stdio.h> 
#include <stdlib.h> 
#include <mpi.h>
#include <time.h>
#define  MASTER 0
#define  NRTASKS 4
#define  SIZE 40
#define  INFINITY 99999 
#define  VERTICES 4


using namespace std;

void rf(int(&dst)[NRTASKS][NRTASKS], int rank);

void main(int argc, char *argv[])
{
	int size, myrank;
	int source; 

	int tag = 1;

	int dst[NRTASKS][NRTASKS];

	MPI_Status status;
	MPI_Init(&argc, &argv);
	MPI_Comm_rank(MPI_COMM_WORLD, &myrank);
	MPI_Comm_size(MPI_COMM_WORLD, &size);


	if (myrank == MASTER)
	{
		for (int line = 0; line < NRTASKS; ++line) {
			for (int col = 0; col < NRTASKS; ++col) {
				scanf("%d",dst[line][col]);
			}
		}
		
		rf(dst,myrank);
		for (int processIndex = 1; processIndex < NRTASKS; ++processIndex) {
			source = processIndex;

			MPI_Send(&dst, NRTASKS*NRTASKS, MPI_INT, processIndex, tag, MPI_COMM_WORLD);

			printf("Master sent graph to process %d\n", processIndex);

			MPI_Recv(&dst, NRTASKS*NRTASKS, MPI_INT, source, tag, MPI_COMM_WORLD, &status);
		}

		for (int line = 0; line < NRTASKS; ++line) {
			for (int col = 0; col < NRTASKS; ++col) {
				if (dst[line][col] == INFINITY)
					printf("%7s", "INF");
				else
					printf("%7d", dst[line][col]);
			}
			printf("\n");
		}
	}
	else if (myrank != MASTER){
		source = MASTER;

		MPI_Recv(&dst, NRTASKS*NRTASKS, MPI_INT, source, tag, MPI_COMM_WORLD, &status);

		rf(dst, myrank);
		
		MPI_Send(&dst, NRTASKS*NRTASKS, MPI_INT, MASTER, tag, MPI_COMM_WORLD);

	}
	

	MPI_Finalize();
}

void rf(int (&dst)[NRTASKS][NRTASKS],int rank) {
	for (int line = 0; line < NRTASKS; ++line) {
		for (int col = 0; col < NRTASKS; ++col) {
			if (dst[line][rank] + dst[rank][col] < dst[line][col]) {
				dst[line][col] = dst[line][rank] + dst[rank][col];
			}
		}
	}
}
