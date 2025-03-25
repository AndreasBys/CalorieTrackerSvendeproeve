using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Services;

public class BarcodeTaskCompletionService
{
    private TaskCompletionSource<string> _taskCompletionSource;

    public Task<string> StartBarcodeTask()
    {
        _taskCompletionSource = new TaskCompletionSource<string>();
        return _taskCompletionSource.Task;
    }

    public void TaskCompleted(string barcode)
    {
        if (!_taskCompletionSource.Task.IsCompleted)
        {
            _taskCompletionSource.SetResult(barcode);   
        }
    }

}
