namespace Core.CommandHandler
{
    public abstract class HandlerBase
    {
        public Task<OperationResult<T>> CallFunction<T>(Func<T> function)
        {
            try
            {
                return Task.FromResult(OperationResult.Success(function.Invoke()));
            }
            catch (Exception ex)
            {
                return Task.FromResult(OperationResult.Error<T>(ex));
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
