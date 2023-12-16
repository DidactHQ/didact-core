using System.Threading.Tasks;

namespace DidactCore.Flows
{
    public interface IFlowInput
    {
        /// <summary>
        /// <para>Asynchronously configures the inputs of the Flow.</para>
        /// <para>If no asynchronous actions occur in this method, simply end the method with Task.CompletedTask.</para>
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task ConfigureInputsAsync(object[] inputs);
    }
}
