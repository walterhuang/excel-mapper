﻿using System;
using System.Reflection;
using ExcelDataReader;

namespace ExcelMapper.Mappings.Items
{
    public class ParseAsEnumMappingItem : ISinglePropertyMappingItem
    {
        public Type EnumType { get; }

        public ParseAsEnumMappingItem(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }

            if (!enumType.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException($"Type {enumType} is not an Enum.", nameof(enumType));
            }

            EnumType = enumType;
        }

        public PropertyMappingResult GetProperty(ReadResult mapResult)
        {
            try
            {
                object value = Enum.Parse(EnumType, mapResult.StringValue);
                return PropertyMappingResult.Success(value);
            }
            catch
            {
                return PropertyMappingResult.Invalid();
            }
        }
    }
}
