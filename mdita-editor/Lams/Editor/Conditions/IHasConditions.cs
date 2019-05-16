namespace mDitaEditor.Lams.Editor.Conditions
{
    interface IHasConditions
    {
        LamsConditionType[] ConditionsAvailable { get; }
        string ActivityTitle { get; }
    }
}
