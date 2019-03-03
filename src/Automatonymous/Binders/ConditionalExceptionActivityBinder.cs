// Copyright 2011-2016 Chris Patterson, Dru Sellers
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Automatonymous.Binders
{
    using System;
    using Activities;
    using Behaviors;


    public class ConditionalExceptionActivityBinder<TInstance, TException> :
        ActivityBinder<TInstance>
        where TInstance : class
        where TException : Exception
    {
        readonly EventActivities<TInstance> _activities;
        readonly StateMachineCondition<TInstance> _condition;
        readonly Event _event;

        public ConditionalExceptionActivityBinder(Event @event, StateMachineCondition<TInstance> condition, EventActivities<TInstance> activities)
        {
            _activities = activities;
            _condition = condition;
            _event = @event;
        }

        public bool IsStateTransitionEvent(State state)
        {
            return Equals(_event, state.Enter) || Equals(_event, state.BeforeEnter)
                   || Equals(_event, state.AfterLeave) || Equals(_event, state.Leave);
        }

        public void Bind(State<TInstance> state)
        {
            var catchBuilder = new CatchBehaviorBuilder<TInstance>();

            foreach (var activity in _activities.GetStateActivityBinders())
            {
                activity.Bind(catchBuilder);
            }

            var conditionActivity = new ConditionExceptionActivity<TInstance, TException>(_condition, catchBuilder.Behavior);

            state.Bind(_event, conditionActivity);
        }

        public void Bind(BehaviorBuilder<TInstance> builder)
        {
            var catchBuilder = new CatchBehaviorBuilder<TInstance>();

            foreach (var activity in _activities.GetStateActivityBinders())
            {
                activity.Bind(catchBuilder);
            }

            var conditionActivity = new ConditionExceptionActivity<TInstance, TException>(_condition, catchBuilder.Behavior);

            builder.Add(conditionActivity);
        }
    }


    public class ConditionalExceptionActivityBinder<TInstance, TData, TException> :
        ActivityBinder<TInstance>
        where TInstance : class
        where TException : Exception
    {
        readonly EventActivities<TInstance> _activities;
        readonly StateMachineCondition<TInstance, TData> _condition;
        readonly Event _event;

        public ConditionalExceptionActivityBinder(Event @event, StateMachineCondition<TInstance, TData> condition,
            EventActivities<TInstance> activities)
        {
            _activities = activities;
            _condition = condition;
            _event = @event;
        }

        public bool IsStateTransitionEvent(State state)
        {
            return Equals(_event, state.Enter) || Equals(_event, state.BeforeEnter)
                   || Equals(_event, state.AfterLeave) || Equals(_event, state.Leave);
        }

        public void Bind(State<TInstance> state)
        {
            var catchBuilder = new CatchBehaviorBuilder<TInstance>();

            foreach (var activity in _activities.GetStateActivityBinders())
            {
                activity.Bind(catchBuilder);
            }

            var conditionActivity = new ConditionExceptionActivity<TInstance, TData, TException>(_condition, catchBuilder.Behavior);

            state.Bind(_event, conditionActivity);
        }

        public void Bind(BehaviorBuilder<TInstance> builder)
        {
            var catchBuilder = new CatchBehaviorBuilder<TInstance>();

            foreach (var activity in _activities.GetStateActivityBinders())
            {
                activity.Bind(catchBuilder);
            }

            var conditionActivity = new ConditionExceptionActivity<TInstance, TData, TException>(_condition, catchBuilder.Behavior);

            builder.Add(conditionActivity);
        }
    }
}