using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Arenar.Character
{
	public class CharacterRaycastComponent : ICharacterRaycastComponent, IFixedTickable
	{
		private ICharacterEntity characterOwner;
		private CharacterDataStorage characterData;
		private TickableManager tickableManager;
		
		private LayerMask groundLayerMask;
		
		
		public bool IsGrounded { get; private set; }
		public float GroundAngle { get; private set; }
		
		private List<ContactPoint2D> groundContacts = new List<ContactPoint2D>();


		[Inject]
		public void Construct(ICharacterEntity characterOwner,
							TickableManager tickableManager,
							ICharacterDataStorage<CharacterDataStorage> characterDataStorage)
		{
			this.characterOwner = characterOwner;
			this.tickableManager = tickableManager;
			characterData = characterDataStorage.Data;
		}
		
		public void Initialize()
		{
			groundLayerMask = LayerMask.GetMask("Ground");
		}

		public void DeInitialize()
		{
		}

		public void OnActivate()
		{
			tickableManager.AddFixed(this);
		}

		public void OnDeactivate()
		{
			tickableManager.RemoveFixed(this);
		}
		
		public void FixedTick()
		{
			UpdateGroundState();
		}
		
		private void UpdateGroundState()
		{
			if (characterData.CollisionObserver.GroundContacts.Count > 0)
			{
				IsGrounded = true;
				
				Vector2 averageNormal = Vector2.zero;
				foreach (var contact in groundContacts)
				{
					averageNormal += contact.normal;
				}
				averageNormal.Normalize();
				
				GroundAngle = Vector2.Angle(averageNormal, Vector2.up);
			}
			else
			{
				IsGrounded = false;
				GroundAngle = 0f;
			}
		}
	}
}