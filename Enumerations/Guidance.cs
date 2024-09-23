// ******************************************************************************************
//     Assembly:                Ninja
//     Author:                  Terry D. Eppler
//     Created:                 09-23-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        09-23-2024
// ******************************************************************************************
// <copyright file="Guidance.cs" company="Terry D. Eppler">
// 
//    Ninja is a network toolkit, support iperf, tcp, udp, websocket, mqtt,
//    sniffer, pcap, port scan, listen, ip scan .etc.
// 
//    Copyright ©  2019-2024 Terry D. Eppler
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
//    You can contact me at:  terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   Guidance.cs
// </summary>
// ******************************************************************************************

namespace Ninja
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public enum Guidance
    {
        /// <summary>
        /// The advice of allowance fiscal year2021
        /// </summary>
        AdviceOfAllowanceFiscalYear2021,

        /// <summary>
        /// The advice of allowance fiscal year2022
        /// </summary>
        AdviceOfAllowanceFiscalYear2022,

        /// <summary>
        /// The advice of allowance fiscal year2023
        /// </summary>
        AdviceOfAllowanceFiscalYear2023,

        /// <summary>
        /// The anti deficiency act
        /// </summary>
        AntiDeficiencyAct,

        /// <summary>
        /// The budget control act
        /// </summary>
        BudgetControlAct,

        /// <summary>
        /// The clean air act
        /// </summary>
        CleanAirAct,

        /// <summary>
        /// The clean water act
        /// </summary>
        CleanWaterAct,

        /// <summary>
        /// The comprehensive environmental response compensation and liability act
        /// </summary>
        ComprehensiveEnvironmentalResponseCompensationAndLiabilityAct,

        /// <summary>
        /// The data act
        /// </summary>
        DataAct,

        /// <summary>
        /// The deobligation recertification guidance
        /// </summary>
        DeobligationRecertificationGuidance,

        /// <summary>
        /// The emergency response mission assignment guidance
        /// </summary>
        EmergencyResponseMissionAssignmentGuidance,

        /// <summary>
        /// The funds control manual
        /// </summary>
        FundsControlManual,

        /// <summary>
        /// The money and finance
        /// </summary>
        MoneyAndFinance,

        /// <summary>
        /// The object class manual
        /// </summary>
        ObjectClassManual,

        /// <summary>
        /// The official travel
        /// </summary>
        OfficialTravel,

        /// <summary>
        /// The superfund introduction
        /// </summary>
        SuperfundIntroduction,

        /// <summary>
        /// The superfund general information
        /// </summary>
        SuperfundGeneralInformation,

        /// <summary>
        /// The superfund budget and resource management
        /// </summary>
        SuperfundBudgetAndResourceManagement,

        /// <summary>
        /// The superfund cost recovery process
        /// </summary>
        SuperfundCostRecoveryProcess,

        /// <summary>
        /// The superfund direct charging of superfund costs
        /// </summary>
        SuperfundDirectChargingOfSuperfundCosts,

        /// <summary>
        /// The superfund inter-agency agreements
        /// </summary>
        SuperfundInterAgencyAgreements,

        /// <summary>
        /// The superfund personnel and support cost allocation
        /// </summary>
        SuperfundPersonnelAndSupportCostAllocation,

        /// <summary>
        /// The superfund site specific cost accounting
        /// </summary>
        SuperfundSiteSpecificCostAccounting,

        /// <summary>
        /// The superfund site special accounts
        /// </summary>
        SuperfundSiteSpecialAccounts,

        /// <summary>
        /// The superfund state contracts and cooperative agreements
        /// </summary>
        SuperfundStateContractsAndCooperativeAgreements,

        /// <summary>
        /// The superfund state cost share provisions
        /// </summary>
        SuperfundStateCostShareProvisions,

        /// <summary>
        /// The jobs act advice of allowance fiscal year2022
        /// </summary>
        JobsActAdviceOfAllowanceFiscalYear2022,

        /// <summary>
        /// The jobs act advice of allowance fiscal year2023
        /// </summary>
        JobsActAdviceOfAllowanceFiscalYear2023,

        /// <summary>
        /// The inflation reduction act advice of allowance fiscal year2022
        /// </summary>
        InflationReductionActAdviceOfAllowanceFiscalYear2022,

        /// <summary>
        /// The inflation reduction act advice of allowance fiscal year2023
        /// </summary>
        InflationReductionActAdviceOfAllowanceFiscalYear2023,

        /// <summary>
        /// The federal account symbols and titles book
        /// </summary>
        FederalAccountSymbolsAndTitlesBook,

        /// <summary>
        /// The federal trust fund accounting guide
        /// </summary>
        FederalTrustFundAccountingGuide,

        /// <summary>
        /// The managements responsibility for enterprise risk management and internal control
        /// </summary>
        ManagementsResponsibilityForEnterpriseRiskManagementAndInternalControl,

        /// <summary>
        /// The government invoicing user guide
        /// </summary>
        GovernmentInvoicingUserGuide,

        /// <summary>
        /// The federal government standards for internal controls
        /// </summary>
        FederalGovernmentStandardsForInternalControls,

        /// <summary>
        /// The toxic substances control act
        /// </summary>
        ToxicSubstancesControlAct,

        /// <summary>
        /// The government auditing standards
        /// </summary>
        GovernmentAuditingStandards,

        /// <summary>
        /// The incident management handbook
        /// </summary>
        IncidentManagementHandbook,

        /// <summary>
        /// The resource conservation and recovery act
        /// </summary>
        ResourceConservationAndRecoveryAct,

        /// <summary>
        /// The oil pollution act
        /// </summary>
        OilPollutionAct,

        /// <summary>
        /// The pollution prevention act
        /// </summary>
        PollutionPreventionAct,

        /// <summary>
        /// The safe drinking water act
        /// </summary>
        SafeDrinkingWaterAct,

        /// <summary>
        /// The solid waste disposal act
        /// </summary>
        SolidWasteDisposalAct,

        /// <summary>
        /// The superfund amendments and reauthorization act
        /// </summary>
        SuperfundAmendmentsAndReauthorizationAct,

        /// <summary>
        /// The preparation submission and execution of the budget
        /// </summary>
        PreparationSubmissionAndExecutionOfTheBudget,

        /// <summary>
        /// The principles of federal appropriations law volume one
        /// </summary>
        PrinciplesOfFederalAppropriationsLawVolumeOne,

        /// <summary>
        /// The principles of federal appropriations law volume two
        /// </summary>
        PrinciplesOfFederalAppropriationsLawVolumeTwo,

        /// <summary>
        /// The statutory authorities for inter-agency agreements
        /// </summary>
        StatutoryAuthoritiesForInterAgencyAgreements,

        /// <summary>
        /// The superfund annual allocation guidance
        /// </summary>
        SuperfundAnnualAllocationGuidance,

        /// <summary>
        /// IT Code guidance
        /// </summary>
        ItCodeGuidance,

        /// <summary>
        /// The indirect costs of assistance agreements
        /// </summary>
        IndirectCostsOfAssistanceAgreements,

        /// <summary>
        /// The purchase card policy
        /// </summary>
        PurchaseCardPolicy,

        /// <summary>
        /// The separation of duties
        /// </summary>
        SeparationOfDuties,

        /// <summary>
        /// The non-direct costs
        /// </summary>
        NonDirectAllocableCosts,

        /// <summary>
        /// The split funding requirements
        /// </summary>
        SplitFundingRequirements
    }
}