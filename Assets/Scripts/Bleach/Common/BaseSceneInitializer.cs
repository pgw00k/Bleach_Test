using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bleach.Common
{
	// Token: 0x020000A6 RID: 166
	public abstract class BaseSceneInitializer : MonoBehaviour
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x0001955C File Offset: 0x0001775C
		private void Update()
		{
			if (!this.registeredTask || !this.RegisterdTaskEnabled)
			{
				return;
			}
			if (!this.executingTask && !this.completedTask)
			{
				if (this.tasks.Count > 0)
				{
					Action action = this.tasks.Dequeue();
					this.executingTask = true;
					action();
				}
				if (this.tasks.Count == 0 && !this.executingTask)
				{
					this.Terminate();
				}
			}
			if (this.completedTask && this.succeeded)
			{
				this.OnUpdate();
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00019600 File Offset: 0x00017800
		protected void EntryPoint()
		{
			this.RegisteringTaskPhase();
			this.registeredTask = true;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00019610 File Offset: 0x00017810
		protected void RegisterTask(Action task)
		{
			this.tasks.Enqueue(task);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00019620 File Offset: 0x00017820
		protected void CompleteTask()
		{
			this.CompleteTask(true);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001962C File Offset: 0x0001782C
		protected void CompleteTask(bool success)
		{
			this.executingTask = false;
			if (!success)
			{
				this.succeeded = false;
				this.Terminate();
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00019648 File Offset: 0x00017848
		protected virtual void RegisteringTaskPhase()
		{
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0001964C File Offset: 0x0001784C
		protected virtual void OnCompletedTasks(bool success)
		{
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00019650 File Offset: 0x00017850
		protected virtual void OnUpdate()
		{
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00019654 File Offset: 0x00017854
		private void Terminate()
		{
			this.completedTask = true;
			this.OnCompletedTasks(this.succeeded);
		}

		// Token: 0x04000457 RID: 1111
		private Queue<Action> tasks = new Queue<Action>();

		// Token: 0x04000458 RID: 1112
		private bool registeredTask;

		// Token: 0x04000459 RID: 1113
		private bool executingTask;

		// Token: 0x0400045A RID: 1114
		private bool completedTask;

		// Token: 0x0400045B RID: 1115
		private bool succeeded = true;

		// Token: 0x0400045C RID: 1116
		protected bool RegisterdTaskEnabled;
	}
}
