using Notes.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Tests.Common
{
	public abstract class TestCommandBase : IDisposable
	{
		protected readonly NotesDbContext Context;
		
		public TestCommandBase()
		{
			Context = NotesContextFactory.Create();
		}

		public void Dispose()
		{
			NotesContextFactory.Destroy(Context);
		}
	}
}
