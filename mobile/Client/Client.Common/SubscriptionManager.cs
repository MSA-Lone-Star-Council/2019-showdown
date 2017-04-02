using System;
using Common.Common;

namespace Client.Common
{
	public class SubscriptionManager
	{
		IStorage storage;

		public SubscriptionManager(IStorage storage)
		{
			this.storage = storage;
		}

		public bool this[string subscriptionId]
		{
			get
			{
				return storage.GetBool(subscriptionId);
			}
			set
			{
				storage.Save(subscriptionId, value);
			}
		}

		public void ToggleSubscription(string subscriptionId)
		{
			var current = this[subscriptionId];
			this[subscriptionId] = !current;
		}
	}
}
