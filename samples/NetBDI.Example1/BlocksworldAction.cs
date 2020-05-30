using System.Threading.Tasks;

namespace NetBDI.Example1
{
    /// <summary>
    /// An action for this environment
    /// </summary>
    public abstract class BlocksworldAction : STRIPS.Action
    {
        /// <summary>
        /// Execute the action by an agent
        /// </summary>
        /// <param name="agent">The agent who executes the action</param>
        /// <returns>An asynchronous task in which the action is performed</returns>
        public abstract Task Execute(BlocksworldAgent agent);
    }
}