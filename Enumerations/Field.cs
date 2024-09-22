// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="Field.cs" company="Terry D. Eppler">
//    Badger is data analysis and reporting tool for EPA Analysts.
//    Copyright ©  2024  Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the “Software”),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at: terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   Field.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public enum Field
    {
        /// <summary>
        /// The account code
        /// </summary>
        AccountCode,

        /// <summary>
        /// The account name
        /// </summary>
        AccountName,

        /// <summary>
        /// The account number
        /// </summary>
        AccountNumber,

        /// <summary>
        /// The account sequence
        /// </summary>
        AccountSequence,

        /// <summary>
        /// The account status
        /// </summary>
        AccountStatus,

        /// <summary>
        /// The account title
        /// </summary>
        AccountTitle,

        /// <summary>
        /// The account type
        /// </summary>
        AccountType,

        /// <summary>
        /// The accrual controls
        /// </summary>
        AccrualControls,

        /// <summary>
        /// The accrual spending control
        /// </summary>
        AccrualSpendingControl,

        /// <summary>
        /// The action
        /// </summary>
        Action,

        /// <summary>
        /// The action code
        /// </summary>
        ActionCode,

        /// <summary>
        /// The active
        /// </summary>
        Active,

        /// <summary>
        /// The activity code
        /// </summary>
        ActivityCode,

        /// <summary>
        /// The activity name
        /// </summary>
        ActivityName,

        /// <summary>
        /// The actual recoveries trans type
        /// </summary>
        ActualRecoveriesTransType,

        /// <summary>
        /// The agency
        /// </summary>
        Agency,

        /// <summary>
        /// The agency name
        /// </summary>
        AgencyName,

        /// <summary>
        /// The agency sequence
        /// </summary>
        AgencySequence,

        /// <summary>
        /// The agency title
        /// </summary>
        AgencyTitle,

        /// <summary>
        /// The agreement number
        /// </summary>
        AgreementNumber,

        /// <summary>
        /// The ah code
        /// </summary>
        AhCode,

        /// <summary>
        /// The ah name
        /// </summary>
        AhName,

        /// <summary>
        /// The allocation reason
        /// </summary>
        AllocationReason,

        /// <summary>
        /// The analyst
        /// </summary>
        Analyst,

        /// <summary>
        /// The apportionment account code
        /// </summary>
        ApportionmentAccountCode,

        /// <summary>
        /// The appropriation code
        /// </summary>
        AppropriationCode,

        /// <summary>
        /// The appropriation fund
        /// </summary>
        AppropriationFund,

        /// <summary>
        /// The appropriation name
        /// </summary>
        AppropriationName,

        /// <summary>
        /// The approval date
        /// </summary>
        ApprovalDate,

        /// <summary>
        /// The approved by
        /// </summary>
        ApprovedBy,

        /// <summary>
        /// The april
        /// </summary>
        April,

        /// <summary>
        /// The august
        /// </summary>
        August,

        /// <summary>
        /// The authority distribution control
        /// </summary>
        AuthorityDistributionControl,

        /// <summary>
        /// The authority type
        /// </summary>
        AuthorityType,

        /// <summary>
        /// The authority type name
        /// </summary>
        AuthorityTypeName,

        /// <summary>
        /// The availability
        /// </summary>
        Availability,

        /// <summary>
        /// The bbfy
        /// </summary>
        BBFY,

        /// <summary>
        /// The betc
        /// </summary>
        Betc,

        /// <summary>
        /// The BFS
        /// </summary>
        Bfs,

        /// <summary>
        /// The bfy
        /// </summary>
        BFY,

        /// <summary>
        /// The bea category
        /// </summary>
        BeaCategory,

        /// <summary>
        /// The bea category name
        /// </summary>
        BeaCategoryName,

        /// <summary>
        /// The betc name
        /// </summary>
        BetcName,

        /// <summary>
        /// The boc code
        /// </summary>
        BocCode,

        /// <summary>
        /// The boc name
        /// </summary>
        BocName,

        /// <summary>
        /// The budget estimated lower level
        /// </summary>
        BudgetEstimatedLowerLevel,

        /// <summary>
        /// The budget level
        /// </summary>
        BudgetLevel,

        /// <summary>
        /// The budget object class code
        /// </summary>
        BudgetObjectClassCode,

        /// <summary>
        /// The budget object class name
        /// </summary>
        BudgetObjectClassName,

        /// <summary>
        /// The budget year
        /// </summary>
        BudgetYear,

        /// <summary>
        /// The budget year adjustment
        /// </summary>
        BudgetYearAdjustment,

        /// <summary>
        /// The budgeted control
        /// </summary>
        BudgetedControl,

        /// <summary>
        /// The budgeted trans type
        /// </summary>
        BudgetedTransType,

        /// <summary>
        /// The budgeting controls
        /// </summary>
        BudgetingControls,

        /// <summary>
        /// The budget formulation system
        /// </summary>
        BudgetFormulationSystem,

        /// <summary>
        /// The budget accout code
        /// </summary>
        BudgetAccoutCode,

        /// <summary>
        /// The budget account name
        /// </summary>
        BudgetAccountName,

        /// <summary>
        /// The bureau
        /// </summary>
        Bureau,

        /// <summary>
        /// The bureau code
        /// </summary>
        BureauCode,

        /// <summary>
        /// The bureau name
        /// </summary>
        BureauName,

        /// <summary>
        /// The bureau sequence
        /// </summary>
        BureauSequence,

        /// <summary>
        /// The bureau title
        /// </summary>
        BureauTitle,

        /// <summary>
        /// The calendar date
        /// </summary>
        CalendarDate,

        /// <summary>
        /// The cancellation date
        /// </summary>
        CancellationDate,

        /// <summary>
        /// The caption
        /// </summary>
        Caption,

        /// <summary>
        /// The category
        /// </summary>
        Category,

        /// <summary>
        /// The cell number
        /// </summary>
        CellNumber,

        /// <summary>
        /// The cerclis identifier
        /// </summary>
        CerclisId,

        /// <summary>
        /// The charge type
        /// </summary>
        ChargeType,

        /// <summary>
        /// The christmas
        /// </summary>
        Christmas,

        /// <summary>
        /// The city
        /// </summary>
        City,

        /// <summary>
        /// The closed date
        /// </summary>
        ClosedDate,

        /// <summary>
        /// The code
        /// </summary>
        Code,

        /// <summary>
        /// The columbus
        /// </summary>
        Columbus,

        /// <summary>
        /// The comments
        /// </summary>
        Comments,

        /// <summary>
        /// The commitment controls
        /// </summary>
        CommitmentControls,

        /// <summary>
        /// The commitment spending control
        /// </summary>
        CommitmentSpendingControl,

        /// <summary>
        /// The congressional district
        /// </summary>
        CongressionalDistrict,

        /// <summary>
        /// The cost area code
        /// </summary>
        CostAreaCode,

        /// <summary>
        /// The cost area name
        /// </summary>
        CostAreaName,

        /// <summary>
        /// The cost org code
        /// </summary>
        CostOrgCode,

        /// <summary>
        /// The cost org name
        /// </summary>
        CostOrgName,

        /// <summary>
        /// The created by
        /// </summary>
        CreatedBy,

        /// <summary>
        /// The credit indicator
        /// </summary>
        CreditIndicator,

        /// <summary>
        /// The current year adjustment
        /// </summary>
        CurrentYearAdjustment,

        /// <summary>
        /// The cycle
        /// </summary>
        Cycle,

        /// <summary>
        /// The december
        /// </summary>
        December,

        /// <summary>
        /// The decision
        /// </summary>
        Decision,

        /// <summary>
        /// The decrease restriction
        /// </summary>
        DecreaseRestriction,

        /// <summary>
        /// The definition
        /// </summary>
        Definition,

        /// <summary>
        /// The description
        /// </summary>
        Description,

        /// <summary>
        /// The division
        /// </summary>
        Division,

        /// <summary>
        /// The division name
        /// </summary>
        DivisionName,

        /// <summary>
        /// The document type
        /// </summary>
        DocType,

        /// <summary>
        /// The document control number
        /// </summary>
        DocumentControlNumber,

        /// <summary>
        /// The document date
        /// </summary>
        DocumentDate,

        /// <summary>
        /// The document title
        /// </summary>
        DocumentTitle,

        /// <summary>
        /// The document number
        /// </summary>
        DocumentNumber,

        /// <summary>
        /// The document prefix
        /// </summary>
        DocumentPrefix,

        /// <summary>
        /// The document type
        /// </summary>
        DocumentType,

        /// <summary>
        /// The duns number
        /// </summary>
        DunsNumber,

        /// <summary>
        /// The duration
        /// </summary>
        Duration,

        /// <summary>
        /// The ebfy
        /// </summary>
        EBFY,

        /// <summary>
        /// The Ending Fiscal Year
        /// </summary>
        Efy,

        /// <summary>
        /// The email
        /// </summary>
        Email,

        /// <summary>
        /// The enacted date
        /// </summary>
        EnactedDate,

        /// <summary>
        /// The end date
        /// </summary>
        EndDate,

        /// <summary>
        /// The epa site identifier
        /// </summary>
        EpaSiteId,

        /// <summary>
        /// The estimated recoveries budgeting option
        /// </summary>
        EstimatedRecoveriesBudgetingOption,

        /// <summary>
        /// The estimated recoveries spending option
        /// </summary>
        EstimatedRecoveriesSpendingOption,

        /// <summary>
        /// The estimated recoveries trans type
        /// </summary>
        EstimatedRecoveriesTransType,

        /// <summary>
        /// The estimated reimbursements
        /// </summary>
        EstimatedReimbursements,

        /// <summary>
        /// The estimated reimbursements spending option
        /// </summary>
        EstimatedReimbursementsSpendingOption,

        /// <summary>
        /// The estimated reimbursements trans type
        /// </summary>
        EstimatedReimbursementsTransType,

        /// <summary>
        /// The estimated reimursements budgeting option
        /// </summary>
        EstimatedReimursementsBudgetingOption,

        /// <summary>
        /// The expenditure controls
        /// </summary>
        ExpenditureControls,

        /// <summary>
        /// The expenditure spending control
        /// </summary>
        ExpenditureSpendingControl,

        /// <summary>
        /// The expense controls
        /// </summary>
        ExpenseControls,

        /// <summary>
        /// The expense spending control
        /// </summary>
        ExpenseSpendingControl,

        /// <summary>
        /// The expiring year
        /// </summary>
        ExpiringYear,

        /// <summary>
        /// The feburary
        /// </summary>
        Feburary,

        /// <summary>
        /// The field name
        /// </summary>
        FieldName,

        /// <summary>
        /// The financing accounts
        /// </summary>
        FinancingAccounts,

        /// <summary>
        /// The first name
        /// </summary>
        FirstName,

        /// <summary>
        /// The first year
        /// </summary>
        FirstYear,

        /// <summary>
        /// The fiscal year
        /// </summary>
        FiscalYear,

        /// <summary>
        /// The foc code
        /// </summary>
        FocCode,

        /// <summary>
        /// The foc name
        /// </summary>
        FocName,

        /// <summary>
        /// The foot note
        /// </summary>
        FootNote,

        /// <summary>
        /// From to
        /// </summary>
        FromTo,

        /// <summary>
        /// The fte budgeting control
        /// </summary>
        FteBudgetingControl,

        /// <summary>
        /// The fte spending control
        /// </summary>
        FteSpendingControl,

        /// <summary>
        /// The fund code
        /// </summary>
        FundCode,

        /// <summary>
        /// The fund name
        /// </summary>
        FundName,

        /// <summary>
        /// The goal code
        /// </summary>
        GoalCode,

        /// <summary>
        /// The goal name
        /// </summary>
        GoalName,

        /// <summary>
        /// The grant number
        /// </summary>
        GrantNumber,

        /// <summary>
        /// The group
        /// </summary>
        Group,

        /// <summary>
        /// The hr org code
        /// </summary>
        HrOrgCode,

        /// <summary>
        /// The hr org name
        /// </summary>
        HrOrgName,

        /// <summary>
        /// The increase restriction
        /// </summary>
        IncreaseRestriction,

        /// <summary>
        /// The independence
        /// </summary>
        Independence,

        /// <summary>
        /// The january
        /// </summary>
        January,

        /// <summary>
        /// The july
        /// </summary>
        July,

        /// <summary>
        /// The june
        /// </summary>
        June,

        /// <summary>
        /// The juneteenth
        /// </summary>
        Juneteenth,

        /// <summary>
        /// The jurisdiction
        /// </summary>
        Jurisdiction,

        /// <summary>
        /// The justification
        /// </summary>
        Justification,

        /// <summary>
        /// The labor
        /// </summary>
        Labor,

        /// <summary>
        /// The last activity date
        /// </summary>
        LastActivityDate,

        /// <summary>
        /// The last document date
        /// </summary>
        LastDocumentDate,

        /// <summary>
        /// The last name
        /// </summary>
        LastName,

        /// <summary>
        /// The last update
        /// </summary>
        LastUpdate,

        /// <summary>
        /// The last year
        /// </summary>
        LastYear,

        /// <summary>
        /// The laws
        /// </summary>
        Laws,

        /// <summary>
        /// The ledger account code
        /// </summary>
        LedgerAccountCode,

        /// <summary>
        /// The ledger account name
        /// </summary>
        LedgerAccountName,

        /// <summary>
        /// The line
        /// </summary>
        Line,

        /// <summary>
        /// The line category
        /// </summary>
        LineCategory,

        /// <summary>
        /// The line description
        /// </summary>
        LineDescription,

        /// <summary>
        /// The line name
        /// </summary>
        LineName,

        /// <summary>
        /// The line number
        /// </summary>
        LineNumber,

        /// <summary>
        /// The line section
        /// </summary>
        LineSection,

        /// <summary>
        /// The line split
        /// </summary>
        LineSplit,

        /// <summary>
        /// The line title
        /// </summary>
        LineTitle,

        /// <summary>
        /// The line type
        /// </summary>
        LineType,

        /// <summary>
        /// The march
        /// </summary>
        March,

        /// <summary>
        /// The martin luther king
        /// </summary>
        MartinLutherKing,

        /// <summary>
        /// The may
        /// </summary>
        May,

        /// <summary>
        /// The memo requirement
        /// </summary>
        MemoRequirement,

        /// <summary>
        /// The memorial
        /// </summary>
        Memorial,

        /// <summary>
        /// The message
        /// </summary>
        Message,

        /// <summary>
        /// The model
        /// </summary>
        Model,

        /// <summary>
        /// The modified by
        /// </summary>
        ModifiedBy,

        /// <summary>
        /// The modified date
        /// </summary>
        ModifiedDate,

        /// <summary>
        /// The name
        /// </summary>
        Name,

        /// <summary>
        /// The narrative
        /// </summary>
        Narrative,

        /// <summary>
        /// Creates new value.
        /// </summary>
        NewValue,

        /// <summary>
        /// Creates new years.
        /// </summary>
        NewYears,

        /// <summary>
        /// The november
        /// </summary>
        November,

        /// <summary>
        /// The NPL status code
        /// </summary>
        NplStatusCode,

        /// <summary>
        /// The NPL status name
        /// </summary>
        NplStatusName,

        /// <summary>
        /// The NPM code
        /// </summary>
        NpmCode,

        /// <summary>
        /// The NPM name
        /// </summary>
        NpmName,

        /// <summary>
        /// The objective code
        /// </summary>
        ObjectiveCode,

        /// <summary>
        /// The objective name
        /// </summary>
        ObjectiveName,

        /// <summary>
        /// The obligating document number
        /// </summary>
        ObligatingDocumentNumber,

        /// <summary>
        /// The obligation controls
        /// </summary>
        ObligationControls,

        /// <summary>
        /// The obligation spending control
        /// </summary>
        ObligationSpendingControl,

        /// <summary>
        /// The october
        /// </summary>
        October,

        /// <summary>
        /// The office
        /// </summary>
        Office,

        /// <summary>
        /// The old value
        /// </summary>
        OldValue,

        /// <summary>
        /// The omb account
        /// </summary>
        OmbAccount,

        /// <summary>
        /// The omb account code
        /// </summary>
        OmbAccountCode,

        /// <summary>
        /// The omb account name
        /// </summary>
        OmbAccountName,

        /// <summary>
        /// The omb account title
        /// </summary>
        OmbAccountTitle,

        /// <summary>
        /// The omb agency
        /// </summary>
        OmbAgency,

        /// <summary>
        /// The omb agency code
        /// </summary>
        OmbAgencyCode,

        /// <summary>
        /// The omb agency name
        /// </summary>
        OmbAgencyName,

        /// <summary>
        /// The omb bureau
        /// </summary>
        OmbBureau,

        /// <summary>
        /// The omb bureau code
        /// </summary>
        OmbBureauCode,

        /// <summary>
        /// The omb bureau name
        /// </summary>
        OmbBureauName,

        /// <summary>
        /// The operable unit
        /// </summary>
        OperableUnit,

        /// <summary>
        /// The org code
        /// </summary>
        OrgCode,

        /// <summary>
        /// The org name
        /// </summary>
        OrgName,

        /// <summary>
        /// The original action date
        /// </summary>
        OriginalActionDate,

        /// <summary>
        /// The original request date
        /// </summary>
        OriginalRequestDate,

        /// <summary>
        /// The pay period
        /// </summary>
        PayPeriod,

        /// <summary>
        /// The pay type
        /// </summary>
        PayType,

        /// <summary>
        /// The percentage
        /// </summary>
        Percentage,

        /// <summary>
        /// The phone number
        /// </summary>
        PhoneNumber,

        /// <summary>
        /// The pipeline code
        /// </summary>
        PipelineCode,

        /// <summary>
        /// The pipeline description
        /// </summary>
        PipelineDescription,

        /// <summary>
        /// The posted control
        /// </summary>
        PostedControl,

        /// <summary>
        /// The posted trans type
        /// </summary>
        PostedTransType,

        /// <summary>
        /// The posting controls
        /// </summary>
        PostingControls,

        /// <summary>
        /// The pre commitment controls
        /// </summary>
        PreCommitmentControls,

        /// <summary>
        /// The pre commitment spending control
        /// </summary>
        PreCommitmentSpendingControl,

        /// <summary>
        /// The presidents
        /// </summary>
        Presidents,

        /// <summary>
        /// The prevent new use
        /// </summary>
        PreventNewUse,

        /// <summary>
        /// The processed date
        /// </summary>
        ProcessedDate,

        /// <summary>
        /// The profit loss budget option
        /// </summary>
        ProfitLossBudgetOption,

        /// <summary>
        /// The profit loss spending option
        /// </summary>
        ProfitLossSpendingOption,

        /// <summary>
        /// The profit loss trans type
        /// </summary>
        ProfitLossTransType,

        /// <summary>
        /// The program area code
        /// </summary>
        ProgramAreaCode,

        /// <summary>
        /// The program area name
        /// </summary>
        ProgramAreaName,

        /// <summary>
        /// The program name
        /// </summary>
        ProgramName,

        /// <summary>
        /// The program project code
        /// </summary>
        ProgramProjectCode,

        /// <summary>
        /// The program project name
        /// </summary>
        ProgramProjectName,

        /// <summary>
        /// The project code
        /// </summary>
        ProjectCode,

        /// <summary>
        /// The project name
        /// </summary>
        ProjectName,

        /// <summary>
        /// The project type
        /// </summary>
        ProjectType,

        /// <summary>
        /// The project status
        /// </summary>
        ProjectStatus,

        /// <summary>
        /// The project officer last name
        /// </summary>
        ProjectOfficerLastName,

        /// <summary>
        /// The project officer first name
        /// </summary>
        ProjectOfficerFirstName,

        /// <summary>
        /// The project officer phone number
        /// </summary>
        ProjectOfficerPhoneNumber,

        /// <summary>
        /// The project officer mail code
        /// </summary>
        ProjectOfficerMailCode,

        /// <summary>
        /// The public law
        /// </summary>
        PublicLaw,

        /// <summary>
        /// The purchase request
        /// </summary>
        PurchaseRequest,

        /// <summary>
        /// The purpose
        /// </summary>
        Purpose,

        /// <summary>
        /// The rc code
        /// </summary>
        RcCode,

        /// <summary>
        /// The rc name
        /// </summary>
        RcName,

        /// <summary>
        /// The re org code
        /// </summary>
        ReOrgCode,

        /// <summary>
        /// The recoveries carry in amount control
        /// </summary>
        RecoveriesCarryInAmountControl,

        /// <summary>
        /// The recoveries carry in lower level
        /// </summary>
        RecoveriesCarryInLowerLevel,

        /// <summary>
        /// The recoveries carry in lower level control
        /// </summary>
        RecoveriesCarryInLowerLevelControl,

        /// <summary>
        /// The recovery budget mismatch
        /// </summary>
        RecoveryBudgetMismatch,

        /// <summary>
        /// The recovery next level
        /// </summary>
        RecoveryNextLevel,

        /// <summary>
        /// The reimbursable agreement controls
        /// </summary>
        ReimbursableAgreementControls,

        /// <summary>
        /// The reimbursable agreement number
        /// </summary>
        ReimbursableAgreementNumber,

        /// <summary>
        /// The reimbursable agreement spending control
        /// </summary>
        ReimbursableAgreementSpendingControl,

        /// <summary>
        /// The reimbursable spending control
        /// </summary>
        ReimbursableSpendingControl,

        /// <summary>
        /// The reimbursement controls
        /// </summary>
        ReimbursementControls,

        /// <summary>
        /// The report year
        /// </summary>
        ReportYear,

        /// <summary>
        /// The reprogramming number
        /// </summary>
        ReprogrammingNumber,

        /// <summary>
        /// The reprogramming restriction
        /// </summary>
        ReprogrammingRestriction,

        /// <summary>
        /// The request date
        /// </summary>
        RequestDate,

        /// <summary>
        /// The request number
        /// </summary>
        RequestNumber,

        /// <summary>
        /// The request type
        /// </summary>
        RequestType,

        /// <summary>
        /// The requested by
        /// </summary>
        RequestedBy,

        /// <summary>
        /// The request document
        /// </summary>
        RequestDocument,

        /// <summary>
        /// The resource type
        /// </summary>
        ResourceType,

        /// <summary>
        /// The responsibility center code
        /// </summary>
        ResponsibilityCenterCode,

        /// <summary>
        /// The responsibility center name
        /// </summary>
        ResponsibilityCenterName,

        /// <summary>
        /// The rpio activity code
        /// </summary>
        RpioActivityCode,

        /// <summary>
        /// The rpio activity name
        /// </summary>
        RpioActivityName,

        /// <summary>
        /// The rpio code
        /// </summary>
        RpioCode,

        /// <summary>
        /// The rpio name
        /// </summary>
        RpioName,

        /// <summary>
        /// The rule description
        /// </summary>
        RuleDescription,

        /// <summary>
        /// The rule number
        /// </summary>
        RuleNumber,

        /// <summary>
        /// The ssid
        /// </summary>
        Ssid,

        /// <summary>
        /// The stat
        /// </summary>
        STAT,

        /// <summary>
        /// The schedule
        /// </summary>
        Schedule,

        /// <summary>
        /// The schedule order
        /// </summary>
        ScheduleOrder,

        /// <summary>
        /// The section
        /// </summary>
        Section,

        /// <summary>
        /// The section name
        /// </summary>
        SectionName,

        /// <summary>
        /// The section number
        /// </summary>
        SectionNumber,

        /// <summary>
        /// The security org
        /// </summary>
        SecurityOrg,

        /// <summary>
        /// The september
        /// </summary>
        September,

        /// <summary>
        /// The short name
        /// </summary>
        ShortName,

        /// <summary>
        /// The site code
        /// </summary>
        SiteCode,

        /// <summary>
        /// The site identifier
        /// </summary>
        SiteId,

        /// <summary>
        /// The site name
        /// </summary>
        SiteName,

        /// <summary>
        /// The site project code
        /// </summary>
        SiteProjectCode,

        /// <summary>
        /// The site project name
        /// </summary>
        SiteProjectName,

        /// <summary>
        /// The sort
        /// </summary>
        Sort,

        /// <summary>
        /// The special account fund
        /// </summary>
        SpecialAccountFund,

        /// <summary>
        /// The special account name
        /// </summary>
        SpecialAccountName,

        /// <summary>
        /// The special account number
        /// </summary>
        SpecialAccountNumber,

        /// <summary>
        /// The spending adjustment trans type
        /// </summary>
        SpendingAdjustmentTransType,

        /// <summary>
        /// The start date
        /// </summary>
        StartDate,

        /// <summary>
        /// The state
        /// </summary>
        State,

        /// <summary>
        /// The state code
        /// </summary>
        StateCode,

        /// <summary>
        /// The state name
        /// </summary>
        StateName,

        /// <summary>
        /// The status
        /// </summary>
        Status,

        /// <summary>
        /// The status reserve trans type
        /// </summary>
        StatusReserveTransType,

        /// <summary>
        /// The sub account
        /// </summary>
        SubAccount,

        /// <summary>
        /// The sub appropriation code
        /// </summary>
        SubAppropriationCode,

        /// <summary>
        /// The sub appropriation name
        /// </summary>
        SubAppropriationName,

        /// <summary>
        /// The sub project code
        /// </summary>
        SubProjectCode,

        /// <summary>
        /// The sub project name
        /// </summary>
        SubProjectName,

        /// <summary>
        /// The sub rc code
        /// </summary>
        SubRcCode,

        /// <summary>
        /// The sub rc name
        /// </summary>
        SubRcName,

        /// <summary>
        /// The subcategory
        /// </summary>
        Subcategory,

        /// <summary>
        /// The subcategory name
        /// </summary>
        SubcategoryName,

        /// <summary>
        /// The subfunction
        /// </summary>
        Subfunction,

        /// <summary>
        /// The subline
        /// </summary>
        Subline,

        /// <summary>
        /// The system
        /// </summary>
        System,

        /// <summary>
        /// The tafs
        /// </summary>
        Tafs,

        /// <summary>
        /// The table name
        /// </summary>
        TableName,

        /// <summary>
        /// The tafs account code
        /// </summary>
        TafsAccountCode,

        /// <summary>
        /// The taxation code
        /// </summary>
        TaxationCode,

        /// <summary>
        /// The thanksgiving
        /// </summary>
        Thanksgiving,

        /// <summary>
        /// The time
        /// </summary>
        Time,

        /// <summary>
        /// The time stamp
        /// </summary>
        TimeStamp,

        /// <summary>
        /// The title
        /// </summary>
        Title,

        /// <summary>
        /// The track agreement lower level
        /// </summary>
        TrackAgreementLowerLevel,

        /// <summary>
        /// The transaction number
        /// </summary>
        TransactionNumber,

        /// <summary>
        /// The transaction type
        /// </summary>
        TransactionType,

        /// <summary>
        /// The transaction type control
        /// </summary>
        TransactionTypeControl,

        /// <summary>
        /// The transaction type name
        /// </summary>
        TransactionTypeName,

        /// <summary>
        /// The treasury account
        /// </summary>
        TreasuryAccount,

        /// <summary>
        /// The treasury account code
        /// </summary>
        TreasuryAccountCode,

        /// <summary>
        /// The treasury account name
        /// </summary>
        TreasuryAccountName,

        /// <summary>
        /// The treasury account title
        /// </summary>
        TreasuryAccountTitle,

        /// <summary>
        /// The treasury agency
        /// </summary>
        TreasuryAgency,

        /// <summary>
        /// The treasury agency code
        /// </summary>
        TreasuryAgencyCode,

        /// <summary>
        /// The treasury appropriation fund symbol
        /// </summary>
        TreasuryAppropriationFundSymbol,

        /// <summary>
        /// The treasury fund code
        /// </summary>
        TreasuryFundCode,

        /// <summary>
        /// The treasury fund name
        /// </summary>
        TreasuryFundName,

        /// <summary>
        /// The treasury symbol
        /// </summary>
        TreasurySymbol,

        /// <summary>
        /// The treausury account code
        /// </summary>
        TreausuryAccountCode,

        /// <summary>
        /// The treausury account name
        /// </summary>
        TreausuryAccountName,

        /// <summary>
        /// The treausury agency code
        /// </summary>
        TreausuryAgencyCode,

        /// <summary>
        /// The type
        /// </summary>
        Type,

        /// <summary>
        /// The type code
        /// </summary>
        TypeCode,

        /// <summary>
        /// The vendor code
        /// </summary>
        VendorCode,

        /// <summary>
        /// The vendor name
        /// </summary>
        VendorName,

        /// <summary>
        /// The veterans
        /// </summary>
        Veterans,

        /// <summary>
        /// The week days
        /// </summary>
        WeekDays,

        /// <summary>
        /// The week ends
        /// </summary>
        WeekEnds,

        /// <summary>
        /// The work code
        /// </summary>
        WorkCode,

        /// <summary>
        /// The work code name
        /// </summary>
        WorkCodeName,

        /// <summary>
        /// The work code short name
        /// </summary>
        WorkCodeShortName,

        /// <summary>
        /// The work days
        /// </summary>
        WorkDays,

        /// <summary>
        /// The work project code
        /// </summary>
        WorkProjectCode,

        /// <summary>
        /// The work project name
        /// </summary>
        WorkProjectName,

        /// <summary>
        /// The year of authority
        /// </summary>
        YearOfAuthority,

        /// <summary>
        /// The year
        /// </summary>
        Year
    }
}