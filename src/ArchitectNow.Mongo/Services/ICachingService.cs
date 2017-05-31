﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectNow.Mongo.Services
{
	public interface ICacheService
	{
		void Add(string key, object value, string regionName = "");
		T Get<T>(string key, string regionName = "");
		object Get(string key, string regionName = "");
		void Remove(string key, string regionName = "");
		void ClearCache();
		void CreateRegion(string regionName);
		void ClearRegion(string regionName);
		void ClearKeys(string prefix);
	}
}

