namespace SpaceShooter
{
	public interface IDamageReceiver
	{
		void TakeDamage(int amount);
        void TakeHealth(int amount);
	}
}
