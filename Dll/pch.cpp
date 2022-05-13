// pch.cpp: файл исходного кода, соответствующий предварительно скомпилированному заголовочному файлу

#include "pch.h"

// При использовании предварительно скомпилированных заголовочных файлов необходим следующий файл исходного кода для выполнения сборки.

extern "C" _declspec(dllexport) double InterpolateMKL(const int length_nonuni, const int length_uni, const double* points, const double* func, double* res)
{
	DFTaskPtr task;

	int error_val = dfdNewTask1D(&task, length_nonuni, points, DF_NON_UNIFORM_PARTITION, 1, func, DF_NO_HINT);
	if (error_val != DF_STATUS_OK)
	{
		return error_val + 0.1;
	}

	double* coeff = new double[(length_nonuni - 1) * DF_PP_CUBIC];
	error_val = dfdEditPPSpline1D(task, DF_PP_CUBIC, DF_PP_NATURAL, DF_BC_FREE_END, NULL, DF_NO_IC, NULL, coeff, DF_NO_HINT);
	if (error_val != DF_STATUS_OK)
	{
		return error_val + 0.2;
	}

	error_val = dfdConstruct1D(task, DF_PP_SPLINE, DF_METHOD_STD);
	if (error_val != DF_STATUS_OK)
	{
		return error_val + 0.3;
	}

	int der_count = 3;
	MKL_INT dorder[] = { 1 , 1 , 1 };
	double segment[] = { points[0],  points[length_nonuni - 1] };
	error_val = dfdInterpolate1D(task, DF_INTERP, DF_METHOD_PP, length_uni, segment, DF_UNIFORM_PARTITION, der_count, dorder, NULL, res, DF_NO_HINT, NULL);
	if (error_val != DF_STATUS_OK)
	{
		return error_val + 0.4;
	}

	error_val = dfDeleteTask(&task);
	if (error_val != DF_STATUS_OK)
	{
		return error_val + 0.5;
	}

	return error_val;
}

extern "C" _declspec(dllexport) double IntegrateMKL(const int length_nonuni, const double* points, const double* func, const double* limits, double* res)
{
	DFTaskPtr task;

	int error_val = dfdNewTask1D(&task, length_nonuni, points, DF_NON_UNIFORM_PARTITION, 1, func, DF_NO_HINT);
	if (error_val != DF_STATUS_OK)
	{
		return error_val - 0.1;
	}

	double* coeff = new double[(length_nonuni - 1) * DF_PP_CUBIC];
	error_val = dfdEditPPSpline1D(task, DF_PP_CUBIC, DF_PP_NATURAL, DF_BC_FREE_END, NULL, DF_NO_IC, NULL, coeff, DF_NO_HINT);
	if (error_val != DF_STATUS_OK)
	{
		return error_val - 0.2;
	}

	error_val = dfdConstruct1D(task, DF_PP_SPLINE, DF_METHOD_STD);
	if (error_val != DF_STATUS_OK)
	{
		return error_val - 0.3;
	}

	double* limit1 = new double[1];
	double* limit2 = new double[1];
	limit1[0] = limits[0];
	limit2[0] = limits[1];
	error_val = dfdIntegrate1D(task, DF_METHOD_PP, 1, limit1, DF_UNIFORM_PARTITION, limit2, DF_UNIFORM_PARTITION, NULL, NULL, res, DF_NO_HINT);
	if (error_val != DF_STATUS_OK)
	{
		return error_val - 0.4;
	}

	error_val = dfDeleteTask(&task);
	if (error_val != DF_STATUS_OK)
	{
		return error_val - 0.5;
	}

	return error_val;
}