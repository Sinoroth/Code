﻿#include "cuda_runtime.h"
#include "device_launch_parameters.h"

#include<iostream>
#define  INFINITY 99999 
#define  NRTASKS 4

using namespace std;

int dst[NRTASKS][NRTASKS] = { { 0,   5,  INFINITY, 10 },

		{ INFINITY, 0,   3, INFINITY },

		{ INFINITY, INFINITY, 0,   1 },

		{ INFINITY, INFINITY, INFINITY, 0 }

		};
int k;

__global__ void MatAdd(int dst[NRTASKS][NRTASKS])
{
	int i = threadIdx.x;
	int j = threadIdx.y;

	dst[i][j] = min(dst[i][j], dst[i][k] + ds[k][j]);
}

int main()
{
	int numBlocks = 1;

	dim3 threadsPerBlock(4, 4);

	for (int k = 0; k < NRTASKS; k++)
		MatAdd << <numBlocks, threadsPerBlock >> >(dst);

}