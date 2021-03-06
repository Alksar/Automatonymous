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
namespace Automatonymous.Accessors
{
    using System.Reflection;
    using GreenPipes;
    using GreenPipes.Internals.Reflection;


    public class IntCompositeEventStatusAccessor<TInstance> :
        CompositeEventStatusAccessor<TInstance>
    {
        readonly ReadWriteProperty<TInstance, int> _property;

        public IntCompositeEventStatusAccessor(PropertyInfo propertyInfo)
        {
            _property = new ReadWriteProperty<TInstance, int>(propertyInfo);
        }

        public CompositeEventStatus Get(TInstance instance)
        {
            return new CompositeEventStatus(_property.Get(instance));
        }

        public void Set(TInstance instance, CompositeEventStatus status)
        {
            _property.Set(instance, status.Bits);
        }

        public void Probe(ProbeContext context)
        {
            context.Add("property", _property.Property.Name);
        }
    }
}