using Grid;
using UUtils;

namespace Actors
{
    public class ObstacleActor : GridActor
    {
        protected override void Start()
        {
            DelayHelper.DelayOneFrame(this, () =>
            {
                base.Start();
                maxHealth = 1;
                currentHealth = maxHealth;
                destructible = false;
                blockGridUnit = true;
                hasStepEffect = false;
            });
        }
    }
}