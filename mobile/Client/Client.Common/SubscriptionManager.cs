using System;
using Common.Common;

namespace Client.Common
{
	public class SubscriptionManager
	{
		const string TagStorageKey = "SUBSCRIBED_TAGS";

		IStorage storage;

		public SubscriptionManager(IStorage storage)
		{
			this.storage = storage;
		}

		public bool this[string subscriptionId]
		{
			get
			{
				var savedTags = storage.GetList(TagStorageKey);
				return savedTags.Contains(subscriptionId);
			}
		}

		public void ToggleSubscription(string subscriptionId)
		{
			var current = this[subscriptionId];

			if (current)
			{
				storage.RemoveFromList(TagStorageKey, subscriptionId);
			}
			else
			{
				storage.AddToList(TagStorageKey, subscriptionId);
			}
		}
	}
}
