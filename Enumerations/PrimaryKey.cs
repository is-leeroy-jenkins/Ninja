// ******************************************************************************************
//     Assembly:                Badger
//     Author:                  Terry D. Eppler
//     Created:                 07-28-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        07-28-2024
// ******************************************************************************************
// <copyright file="PrimaryKey.cs" company="Terry D. Eppler">
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
//   PrimaryKey.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public enum PrimaryKey
    {
        /// <summary>
        /// The accounting events identifier
        /// </summary>
        AccountingEventsId,

        /// <summary>
        /// The accounts identifier
        /// </summary>
        AccountsId,

        /// <summary>
        /// The activity codes identifier
        /// </summary>
        ActivityCodesId,

        /// <summary>
        /// The actuals identifier
        /// </summary>
        ActualsId,

        /// <summary>
        /// The administrative requests identifier
        /// </summary>
        AdministrativeRequestsId,

        /// <summary>
        /// The allocations identifier
        /// </summary>
        AllocationsId,

        /// <summary>
        /// The allowance holders identifier
        /// </summary>
        AllowanceHoldersId,

        /// <summary>
        /// The american rescue plan carryover estimates identifier
        /// </summary>
        AmericanRescuePlanCarryoverEstimatesId,

        /// <summary>
        /// The annual carryover estimates identifier
        /// </summary>
        AnnualCarryoverEstimatesId,

        /// <summary>
        /// The annual carryover survey identifier
        /// </summary>
        AnnualCarryoverSurveyId,

        /// <summary>
        /// The annual reimbursable estimates identifier
        /// </summary>
        AnnualReimbursableEstimatesId,

        /// <summary>
        /// The annual reimbursable survey identifier
        /// </summary>
        AnnualReimbursableSurveyId,

        /// <summary>
        /// The application tables identifier
        /// </summary>
        ApplicationTablesId,

        /// <summary>
        /// The apportionment data identifier
        /// </summary>
        ApportionmentDataId,

        /// <summary>
        /// The appropriation available balances identifier
        /// </summary>
        AppropriationAvailableBalancesId,

        /// <summary>
        /// The appropriation documents identifier
        /// </summary>
        AppropriationDocumentsId,

        /// <summary>
        /// The appropriation level authority identifier
        /// </summary>
        AppropriationLevelAuthorityId,

        /// <summary>
        /// The appropriations identifier
        /// </summary>
        AppropriationsId,

        /// <summary>
        /// The budgetary resource execution identifier
        /// </summary>
        BudgetaryResourceExecutionId,

        /// <summary>
        /// The budget controls identifier
        /// </summary>
        BudgetControlsId,

        /// <summary>
        /// The budget documents identifier
        /// </summary>
        BudgetDocumentsId,

        /// <summary>
        /// The budget object classes identifier
        /// </summary>
        BudgetObjectClassesId,

        /// <summary>
        /// The budget outlays identifier
        /// </summary>
        BudgetOutlaysId,

        /// <summary>
        /// The capital planning investment codes identifier
        /// </summary>
        CapitalPlanningInvestmentCodesId,

        /// <summary>
        /// The carryover apportionments identifier
        /// </summary>
        CarryoverApportionmentsId,

        /// <summary>
        /// The carryover outlays identifier
        /// </summary>
        CarryoverOutlaysId,

        /// <summary>
        /// The carryover requests identifier
        /// </summary>
        CarryoverRequestsId,

        /// <summary>
        /// The changes identifier
        /// </summary>
        ChangesId,

        /// <summary>
        /// The column schema identifier
        /// </summary>
        ColumnSchemaId,

        /// <summary>
        /// The compass errors identifier
        /// </summary>
        CompassErrorsId,

        /// <summary>
        /// The compass levels identifier
        /// </summary>
        CompassLevelsId,

        /// <summary>
        /// The compass outlays identifier
        /// </summary>
        CompassOutlaysId,

        /// <summary>
        /// The congressional controls identifier
        /// </summary>
        CongressionalControlsId,

        /// <summary>
        /// The congressional reprogrammings identifier
        /// </summary>
        CongressionalReprogrammingsId,

        /// <summary>
        /// The contacts identifier
        /// </summary>
        ContactsId,

        /// <summary>
        /// The cost areas identifier
        /// </summary>
        CostAreasId,

        /// <summary>
        /// The data rule descriptions identifier
        /// </summary>
        DataRuleDescriptionsId,

        /// <summary>
        /// The defactos identifier
        /// </summary>
        DefactosId,

        /// <summary>
        /// The deobligation activity identifier
        /// </summary>
        DeobligationActivityId,

        /// <summary>
        /// The deobligations identifier
        /// </summary>
        DeobligationsId,

        /// <summary>
        /// The document control numbers identifier
        /// </summary>
        DocumentControlNumbersId,

        /// <summary>
        /// The documents identifier
        /// </summary>
        DocumentsId,

        /// <summary>
        /// The earmark codes identifier
        /// </summary>
        EarmarkCodesId,

        /// <summary>
        /// The earmarks identifier
        /// </summary>
        EarmarksId,

        /// <summary>
        /// The expenditures identifier
        /// </summary>
        ExpendituresId,

        /// <summary>
        /// The federal holidays identifier
        /// </summary>
        FederalHolidaysId,

        /// <summary>
        /// The finance object classes identifier
        /// </summary>
        FinanceObjectClassesId,

        /// <summary>
        /// The fiscal years identifier
        /// </summary>
        FiscalYearsId,

        /// <summary>
        /// The fiscal years back up identifier
        /// </summary>
        FiscalYearsBackUpId,

        /// <summary>
        /// The fund categories identifier
        /// </summary>
        FundCategoriesId,

        /// <summary>
        /// The funds identifier
        /// </summary>
        FundsId,

        /// <summary>
        /// The fund symbols identifier
        /// </summary>
        FundSymbolsId,

        /// <summary>
        /// The general ledger accounts identifier
        /// </summary>
        GeneralLedgerAccountsId,

        /// <summary>
        /// The goals identifier
        /// </summary>
        GoalsId,

        /// <summary>
        /// The gross authority identifier
        /// </summary>
        GrossAuthorityId,

        /// <summary>
        /// The gross utilization identifier
        /// </summary>
        GrossUtilizationId,

        /// <summary>
        /// The growth rates identifier
        /// </summary>
        GrowthRatesId,

        /// <summary>
        /// The gs pay scales identifier
        /// </summary>
        GsPayScalesId,

        /// <summary>
        /// The headquarters authority identifier
        /// </summary>
        HeadquartersAuthorityId,

        /// <summary>
        /// The headquarters offices identifier
        /// </summary>
        HeadquartersOfficesId,

        /// <summary>
        /// The human resource organizations identifier
        /// </summary>
        HumanResourceOrganizationsId,

        /// <summary>
        /// The images identifier
        /// </summary>
        ImagesId,

        /// <summary>
        /// The inflation reduction act carryover estimates identifier
        /// </summary>
        InflationReductionActCarryoverEstimatesId,

        /// <summary>
        /// The jobs act carryover estimates identifier
        /// </summary>
        JobsActCarryoverEstimatesId,

        /// <summary>
        /// The messages identifier
        /// </summary>
        MessagesId,

        /// <summary>
        /// The monthly actuals identifier
        /// </summary>
        MonthlyActualsId,

        /// <summary>
        /// The monthly ledger account balances identifier
        /// </summary>
        MonthlyLedgerAccountBalancesId,

        /// <summary>
        /// The monthly outlays identifier
        /// </summary>
        MonthlyOutlaysId,

        /// <summary>
        /// The national programs identifier
        /// </summary>
        NationalProgramsId,

        /// <summary>
        /// The object class outlays identifier
        /// </summary>
        ObjectClassOutlaysId,

        /// <summary>
        /// The objectives identifier
        /// </summary>
        ObjectivesId,

        /// <summary>
        /// The obligation activity identifier
        /// </summary>
        ObligationActivityId,

        /// <summary>
        /// The obligations identifier
        /// </summary>
        ObligationsId,

        /// <summary>
        /// The open commitments identifier
        /// </summary>
        OpenCommitmentsId,

        /// <summary>
        /// The operating plans identifier
        /// </summary>
        OperatingPlansId,

        /// <summary>
        /// The operating plan updates identifier
        /// </summary>
        OperatingPlanUpdatesId,

        /// <summary>
        /// The organizations identifier
        /// </summary>
        OrganizationsId,

        /// <summary>
        /// The pay periods identifier
        /// </summary>
        PayPeriodsId,

        /// <summary>
        /// The payroll activity identifier
        /// </summary>
        PayrollActivityId,

        /// <summary>
        /// The payroll authority identifier
        /// </summary>
        PayrollAuthorityId,

        /// <summary>
        /// The payroll cost codes identifier
        /// </summary>
        PayrollCostCodesId,

        /// <summary>
        /// The payroll requests identifier
        /// </summary>
        PayrollRequestsId,

        /// <summary>
        /// The PRC identifier
        /// </summary>
        PRCId,

        /// <summary>
        /// The program areas identifier
        /// </summary>
        ProgramAreasId,

        /// <summary>
        /// The program financing schedule identifier
        /// </summary>
        ProgramFinancingScheduleId,

        /// <summary>
        /// The program project descriptions identifier
        /// </summary>
        ProgramProjectDescriptionsId,

        /// <summary>
        /// The program projects identifier
        /// </summary>
        ProgramProjectsId,

        /// <summary>
        /// The project cost codes identifier
        /// </summary>
        ProjectCostCodesId,

        /// <summary>
        /// The projects identifier
        /// </summary>
        ProjectsId,

        /// <summary>
        /// The providers identifier
        /// </summary>
        ProvidersId,

        /// <summary>
        /// The public laws identifier
        /// </summary>
        PublicLawsId,

        /// <summary>
        /// The query definitions identifier
        /// </summary>
        QueryDefinitionsId,

        /// <summary>
        /// The recovery act identifier
        /// </summary>
        RecoveryActId,

        /// <summary>
        /// The reference tables identifier
        /// </summary>
        ReferenceTablesId,

        /// <summary>
        /// The regional authority identifier
        /// </summary>
        RegionalAuthorityId,

        /// <summary>
        /// The regional offices identifier
        /// </summary>
        RegionalOfficesId,

        /// <summary>
        /// The reimbursable agreements identifier
        /// </summary>
        ReimbursableAgreementsId,

        /// <summary>
        /// The reimbursable funds identifier
        /// </summary>
        ReimbursableFundsId,

        /// <summary>
        /// The reports identifier
        /// </summary>
        ReportsId,

        /// <summary>
        /// The reprogrammings identifier
        /// </summary>
        ReprogrammingsId,

        /// <summary>
        /// The resource planning offices identifier
        /// </summary>
        ResourcePlanningOfficesId,

        /// <summary>
        /// The resources identifier
        /// </summary>
        ResourcesId,

        /// <summary>
        /// The responsibility centers identifier
        /// </summary>
        ResponsibilityCentersId,

        /// <summary>
        /// The schema types identifier
        /// </summary>
        SchemaTypesId,

        /// <summary>
        /// The site activity identifier
        /// </summary>
        SiteActivityId,

        /// <summary>
        /// The site project codes identifier
        /// </summary>
        SiteProjectCodesId,

        /// <summary>
        /// The special accounts identifier
        /// </summary>
        SpecialAccountsId,

        /// <summary>
        /// The spending documents identifier
        /// </summary>
        SpendingDocumentsId,

        /// <summary>
        /// The spending rates identifier
        /// </summary>
        SpendingRatesId,

        /// <summary>
        /// The state grant obligations identifier
        /// </summary>
        StateGrantObligationsId,

        /// <summary>
        /// The state organizations identifier
        /// </summary>
        StateOrganizationsId,

        /// <summary>
        /// The status of american rescue plan funds identifier
        /// </summary>
        StatusOfAmericanRescuePlanFundsId,

        /// <summary>
        /// The status of appropriations identifier
        /// </summary>
        StatusOfAppropriationsId,

        /// <summary>
        /// The status of budgetary resources identifier
        /// </summary>
        StatusOfBudgetaryResourcesId,

        /// <summary>
        /// The status of earmarks identifier
        /// </summary>
        StatusOfEarmarksId,

        /// <summary>
        /// The status of funds identifier
        /// </summary>
        StatusOfFundsId,

        /// <summary>
        /// The status of inflation reduction act funds identifier
        /// </summary>
        StatusOfInflationReductionActFundsId,

        /// <summary>
        /// The status of jobs act funds identifier
        /// </summary>
        StatusOfJobsActFundsId,

        /// <summary>
        /// The status of supplemental funds identifier
        /// </summary>
        StatusOfSupplementalFundsId,

        /// <summary>
        /// The sub appropriations identifier
        /// </summary>
        SubAppropriationsId,

        /// <summary>
        /// The superfund sites identifier
        /// </summary>
        SuperfundSitesId,

        /// <summary>
        /// The supplemental carryover estimates identifier
        /// </summary>
        SupplementalCarryoverEstimatesId,

        /// <summary>
        /// The supplemental reimburseable estimates identifier
        /// </summary>
        SupplementalReimburseableEstimatesId,

        /// <summary>
        /// The transfer activity identifier
        /// </summary>
        TransferActivityId,

        /// <summary>
        /// The transfers identifier
        /// </summary>
        TransfersId,

        /// <summary>
        /// The trans types identifier
        /// </summary>
        TransTypesId,

        /// <summary>
        /// The treasury symbols identifier
        /// </summary>
        TreasurySymbolsId,

        /// <summary>
        /// The unliquidated obligations identifier
        /// </summary>
        UnliquidatedObligationsId,

        /// <summary>
        /// The unobligated authority identifier
        /// </summary>
        UnobligatedAuthorityId,

        /// <summary>
        /// The unobligated balances identifier
        /// </summary>
        UnobligatedBalancesId,

        /// <summary>
        /// The URL identifier
        /// </summary>
        UrlId,

        /// <summary>
        /// The work codes identifier
        /// </summary>
        WorkCodesId,

        /// <summary>
        /// The full time equivalents identifier
        /// </summary>
        FullTimeEquivalentsId
    }
}