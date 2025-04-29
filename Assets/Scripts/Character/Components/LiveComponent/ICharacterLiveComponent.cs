using System;

namespace Arenar.Character
{
	public interface ICharacterLiveComponent : ICharacterComponent
	{
		event Action<ICharacterEntity> OnCharacterDeath;
		event Action<int, int> OnCharacterChangeHealthValue;


		int Health { get; set; }
		int HealthMax { get; set; }
		void GetDamage(int damage = 1);
	}
}