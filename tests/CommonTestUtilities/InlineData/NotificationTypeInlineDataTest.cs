using System.Collections;
using System.Collections.Generic;

namespace CommonTestUtilities.InlineData;

public class NotificationTypeInlineDataTest : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "Email" };
        yield return new object[] { "Sms" };
        yield return new object[] { "Push" };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
