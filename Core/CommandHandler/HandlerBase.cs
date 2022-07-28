namespace Core.CommandHandler
{
    public abstract class HandlerBase
    {
        public Task<OperationResult<T>> CallFunction<T>(Func<T> function, Exception ex = null)
        {
            try
            {
                return Task.FromResult(OperationResult.Success(function.Invoke()));
            }
            catch (Exception)
            {
                return Task.FromResult(OperationResult.Error<T>(ex));
            }
        }

        public Task<OperationResult<T>> CallFunction<T>(Func<Task<OperationResult<T>>> function)
        {
            try
            {
                return function.Invoke();
            }
            catch (Exception e)
            {
                return Task.FromResult(OperationResult.Error<T>(e));
            }
        }

        public Task<OperationResult> CallAction(Action action)
        {
            try
            {
                action.Invoke();
                return Task.FromResult(OperationResult.Success());
            }
            catch (Exception e)
            {
                return Task.FromResult(OperationResult.Error(e));
            }
        }
    }
}
