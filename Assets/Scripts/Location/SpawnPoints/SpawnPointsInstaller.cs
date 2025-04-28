using Arenar.Location;
using UnityEngine;
using Zenject;


namespace Arenar.Installers
{
	public class SpawnPointsInstaller : MonoInstaller
	{
		[SerializeField]
		private PlayerSpawnPoint playerSpawnPoint;
		[SerializeField]
		private EnemiesSpawnPoints enemiesSpawnPoints;
		
		
		public override void InstallBindings()
		{
			Container.Bind<PlayerSpawnPoint>()
				.FromInstance(playerSpawnPoint)
				.AsSingle().NonLazy();
			
			Container.Bind<EnemiesSpawnPoints>()
				.FromInstance(enemiesSpawnPoints)
				.AsSingle().NonLazy();
		}
	}
}