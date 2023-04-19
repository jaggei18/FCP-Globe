<template>
  <q-page>
    <q-linear-progress v-if="isLoading" indeterminate rounded color="negative" class="mt-4" />
    <div class="row justify-around">
      <div class="col-sm-4 col-lg-4 mt-3">
        <div class="mainTitleBg">Policy Inquiry</div>
        <div class="mainBoxBg policyBoxRadius">
          <div class="row"></div>
          <p>Key in a new Policy Number</p>
          <form class="row g-3 formBorderBottom padding-btm">
            <div class="col-4">
              <label>Policy Number</label>
            </div>
            <div class="col-5 padding-space">
              <q-input
                outlined
                v-model="policyNumberTxt"
                class="form-control"
                dense
              />
              <span v-if="isPolicyNumberEmpty" style="color:red">Policy number is required</span>
              <!-- :rules="[val => val!='' || 'Policy number is required']" -->
            </div>
            <div class="col-2 padding-space">
              <q-btn
                color="primary"
                label="Submit"
                @click="getPolicyInqVerifyInfo(policyNumberTxt);"
                type="button"
                class="btn btn-primary btn-sm mb-3 mt-1"
              />
            </div>
          </form>
          <div class="pt-3 mt-3">
            <p>View a Pending Policy Number</p>
            <form class="row g-3 padding-btm">
              <div class="col-4">
                <label>Agent Number</label>
              </div>
              <div class="col-5 padding-space">
                <q-input
                  outlined
                  v-model="agentNumberTxt"
                  class="form-control"
                  dense
                />
                <!-- :rules="[val => val!='' || 'Agent number is required']" -->
              </div>
              <div class="col-2 padding-space">
                <q-btn
                  color="primary"
                  @click="getAgentInfo()"
                  label="Submit"
                  type="button"
                  class="btn btn-primary btn-sm mb-3 mt-1"
                />
              </div>
            </form>
          </div>
        </div>
        <div class="mainFooterBg">
          To submit questions or information on existing or pending policies.
        </div>
        <div v-if="blockType === 'showPolicyFull'">
          <div class="mt-3">
            <div class="mainTitleBg">BILLING INFORMATION</div>
            <div class="mainBoxBg1">
              <div class="row mt-2 mb-2">
                <div class="box col-6 text-end">
                  <label>Type:</label>
                </div>
                <div class="box col-5 text-end textBG">
                  <a href="javascript:void(0)" @click="getBankDraftInfo('2', policyInfoData[0]);getBankDraftInfo('3', policyInfoData[0])">{{ policyInfoData[0]?.BillTypeDescription }}</a>
                </div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-6 text-end">
                  <label>Mode:</label>
                </div>
                <div class="box col-5 text-end textBG">
                  {{ policyInfoData[0]?.FrequencyDescription }}
                </div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-6 text-end">
                  <label>Amount:</label>
                </div>
                <div class="box col-5 text-end textBG">
                  ${{ policyInfoData[0]?.Modal_Premium.toFixed(2) }}
                </div>
              </div>
            </div>
          </div>
        </div>
        <div v-if="blockType === 'showPolicyBankDraftBill'">
          <div class="mt-3">
            <div class="mainTitleBg">BILLING INFORMATION</div>
            <div class="mainBoxBg1">
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Type:</label>
                </div>
                <div class="box col-6 text-end textBG">
                  <a href="javascript:void(0)" @click="getBankDraftInfo('', policyBankDraftBill[0]);">{{ policyBankDraftBill[0]?.BillTypeDescription }}</a>
                </div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Mode:</label>
                </div>
                <div class="box col-6 text-end textBG">
                  {{ policyBankDraftBill[0]?.FrequencyDescription }}
                </div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Amount:</label>
                </div>
                <div class="box col-6 text-end textBG">
                  ${{ policyBankDraftBill[0]?.Modal_Premium.toFixed(2) }}
                </div>
              </div>
            </div>
          </div>

          <div class="mt-3">
            <div class="mainTitleBg">PAYMENT INFORMATION</div>
            <div class="mainBoxBg1">
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Amount Received:</label>
                </div>
                <div class="box col-6 text-end textBG">${{policyBankDraftBill[0]?.Last_Payment_Amount.toFixed(2)}}</div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Date Applied:</label>
                </div>
                <div class="box col-6 text-end textBG"> {{formatDateValue(policyBankDraftBill[0]?.Last_Payment_Date)}}</div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Paid To:</label>
                </div>
                <div class="box col-6 text-end textBG">{{formatDateValue(policyBankDraftBill[0]?.PaidTo_Date)}}</div>
              </div>
            </div>
          </div>
          <div class="mt-3">
            <div class="mainTitleBg">
              CASH VALUE & LOAN DATA as of  {{ formatDateValue(policyBankDraftBill[0]?.PaidTo_Date) }}
            </div>
            <div class="mainBoxBg1">
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Gross Cash Value:</label>
                </div>
                <div class="box col-6 text-end textBG">${{policyBankDraftBill[0]?.Cash_Value.toFixed(2)}}</div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Less Loan Payoff Amount:</label>
                </div>
                <div class="box col-6 text-end textBG">${{policyBankDraftBill[0]?.Loan_PayOff.toFixed(2)}}</div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Net Cash Value:</label>
                </div>
                <div class="box col-6 text-end textBG">${{(policyBankDraftBill[0]?.Cash_Value - policyBankDraftBill[0]?.Loan_PayOff).toFixed(2)}}</div>
              </div>
              <div class="row mt-2 mb-2">
                <div class="box col-5 text-end">
                  <label>Loan Value Available:</label>
                </div>
                <div class="box col-6 text-end textBG"> ${{ policyBankDraftBill[0]?.Loan_Value.toFixed(2) }}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-sm-7 col-lg-7 mt-3" v-if="blockType === ''">
        <div class="message-bar" v-show="agentInfoData?.length === 0">
          <div class="mainTitleBg text-start">Messages</div>
          <div class="mainBoxBg mainBoxRightBg">
            <p>{{globalErrorMessage}}</p>
          </div>
        </div>
      </div>
      <div
        class="col-sm-7 col-lg-7 mt-3 agent-bar"
        v-if="blockType === 'showPolicyDetails'"
      >
        <div class="row">
          <h5 class="title">Pending Policy Register</h5>
        </div>
        <div class="mainTitleBg text-start">
          Register Agent for Agent Number {{ agentNumberTxt }}
        </div>
        <div class="row">
          <div class="box col-sm-4 col-lg-4 mt-3">
            <div class="boxTitleBg">PENDING POLICY NUMBER</div>
            <div class="boxTextBg fw-bold polnum">
              <p v-for="item in agentInfoData" :key="item?.PolicyNumber">
                <a
                  href="javascript:void(0)"
                  @click="getPendingPolicyInfo(item?.PolicyNumber)"
                  >{{ item?.PolicyNumber }}</a
                >
              </p>
            </div>
          </div>
          <div class="box col-sm-4 col-lg-4 mt-3">
            <div class="boxTitleBg">INSURED'S NAME</div>
            <div class="boxTextBg dateTxt">
              <p v-for="item in agentInfoData" :key="item?.PolicyNumber">
                {{ item?.Insured }}
              </p>
            </div>
          </div>
          <div class="box col-sm-4 col-lg-4 mt-3">
            <div class="boxTitleBg">DATE PENDING</div>
            <div class="boxTextBg premium">
              <p
                class="text-center"
                v-for="item in agentInfoData"
                :key="item?.PolicyNumber"
              >
                {{ formatDateValue(item?.Last_Payment_Date) }}
              </p>
            </div>
          </div>
        </div>
      </div>
      <div
        class="col-sm-7 col-lg-7 mt-3 agent-bar"
        v-if="blockType === 'showPolicyFull'"
      >
        <div class="row">
          <h5 class="title">Pending Policy Information</h5>
        </div>
        <div class="mainTitleBg text-start">BASIC POLICY INFORMATION</div>
        <div class="mainBoxBg mainBoxRightBg p-3">
          <div class="row mt-2">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label>Policy Number:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyInfoData[0]?.PolicyNumber }}
            </div>
            <div class="box col-sm-2 col-lg-2 text-end">
              <label>Company:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyInfoData[0]?.CompanyName }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label> Policy Owner:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyInfoData[0]?.Owner }}
            </div>
            <div class="box col-sm-2 col-lg-2 text-end">
              <label> Issue Date:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ formatDateValue(policyInfoData[0]?.Issue_Date) }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label> Cash With Application:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ formatDateValue(policyInfoData[0]?.Last_Payment_Date) }}
            </div>
            <div class="box col-sm-2 col-lg-2 text-end">
              <label> UnderWriter:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyInfoData[0]?.Allotment_Payor }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label> Status:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyInfoData[0]?.StatusDesc }}
            </div>
          </div>
        </div>
        <div class="mt-3">
          <div class="mainTitleBg text-start">INSURED INFORMATION</div>
          <div class="mainBoxBg mainBoxRightBg p-3">
            <div class="row mt-2">
              <div class="col-sm-6">
                <div class="row mb-3 mt-2">
                  <div class="box col-sm-6 col-lg-6 text-end">
                    <label> Insured Name:</label>
                  </div>
                  <div class="box col-sm-6 col-lg-6 text-end textBG">
                    {{ policyInfoData[0]?.Insured }}
                  </div>
                </div>
                <div class="row mb-3 mt-2">
                  <div class="box col-sm-6 col-lg-6 text-end">
                    <label> Birth Date:</label>
                  </div>
                  <div class="box col-sm-6 col-lg-6 text-end textBG">
                    {{ formatDateValue(policyInfoData[0]?.DOB) }}
                  </div>
                </div>
                <div class="row mb-3 mt-2">
                  <div class="box col-sm-6 col-lg-6 text-end">
                    <label> Issue Age:</label>
                  </div>
                  <div class="box col-sm-6 col-lg-6 text-end textBG">
                    {{ policyInfoData[0]?.Issue_Age }}
                  </div>
                </div>
              </div>
              <div class="col-sm-6 col-lg-6">
                <div class="row mt-2">
                  <div class="box col-sm-4 col-lg-4 text-end">
                    <label> Address:</label>
                  </div>
                  <div class="box col-sm-6 col-lg-6 text-end textBG">
                    {{ policyInfoData[0]?.Address_1 }}
                    {{ policyInfoData[0]?.Address_2 }}
                    {{ policyInfoData[0]?.Address_3 }}
                    {{ policyInfoData[0]?.Address_4 }}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="titleBGg mt-3">
          COVERAGE INFORMATON as of
          {{ formatDateValue(policyInfoData[0]?.Cycle_Date) }}
        </div>
        <div class="row coverageInfo">
          <div class="box col-lg-4 col-sm-7 mt-3">
            <div class="boxTitleBg">DESCRIPTION</div>
            <div class="boxTextBg fw-bold">
              <p v-for="(item, index) in policyInfoCoverageData" :key="index">
                <a href="javascript:void(0)">{{ item?.CovDesc }}</a>
              </p>
            </div>
          </div>
          <div class="box col-lg-4 col-sm-2 mt-3">
            <div class="boxTitleBg">FACE</div>
            <div class="boxTextBg current">
              <p
                class="text-center"
                v-for="(item, index) in policyInfoCoverageData"
                :key="index"
              >
                {{ item?.Face_Amount }}
              </p>
            </div>
          </div>
          <div class="box col-lg-4 col-sm-3 mt-3">
            <div class="boxTitleBg">ANNUAL PREMIUM</div>
            <div class="boxTextBg premium">
              <p v-for="(item, index) in policyInfoCoverageData" :key="index">
                ${{ item?.Annual_Premium.toFixed(2) }}
              </p>
            </div>
          </div>
        </div>
        <div class="row">
          <p class="box col-sm-12 text-end pr-2">
            <strong>Total: ${{ totalAnnualPremium }} </strong>
          </p>
        </div>
      </div>
      <div
        class="bank-draft col-sm-7 col-lg-7 mt-3"
        v-if="blockType === 'showBankDetails'"
      >
        <div class="row">
          <h5 class="title">Bank Draft</h5>
        </div>
        <div class="mainTitleBg text-start">BANK DRAFT INFORMATION</div>
        <div class="mainBoxBg mainBoxRightBg p-3">
          <div class="row">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label>Payer:</label>
            </div>
            <div class="box col-lg-8 col-sm-8 text-end textBG">
              {{ bankDraftData[0]?.Draft_Payor }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-lg-3 col-sm-3 text-end">
              <label>Company:</label>
            </div>
            <div class="box col-lg-8 col-sm-8 text-end textBG">
              {{ bankDraftData[0]?.CompanyName }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-lg-3 col-sm-3 text-end">
              <label>Bank/Credit Union:</label>
            </div>
            <div class="box col-lg-8 col-sm-8 text-end textBG">
              {{ bankDraftData[0]?.Draft_Bank }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-lg-3 col-sm-3 text-end">
              <label>Mode:</label>
            </div>
            <div class="box col-lg-2 col-sm-2 text-end textBG">
              {{ bankDraftData[0]?.FrequencyDescription }}
            </div>

            <div class="box col-lg-4 col-sm-4 text-end">
              <label>Draft Day:</label>
            </div>
            <div class="box col-lg-2 col-sm-2 text-end textBG">
              {{ bankDraftData[0]?.Draft_Day }}
            </div>
          </div>

          <div class="row mt-2">
            <div class="box col-lg-3 col-sm-3 text-end">
              <label>Policies on Draft:</label>
            </div>
            <div class="box col-lg-2 col-sm-2 text-end textBG">
              {{ bankDraftPolicyDetails?.length }}
            </div>

            <div class="box col-lg-4 col-sm-4 text-end">
              <label>Total Draft Premium:</label>
            </div>
            <div class="box col-lg-2 col-sm-2 text-end textBG">
              ${{ totalDraftPremium }}
            </div>
          </div>
        </div>

        <div class="titleBGg mt-3">BANK DRAFT POLICY DETAIL</div>
        <p class="pt-3 mb-0 fw-bold">
          Select a policy below to view the policy detail.
        </p>
        <div class="row" v-if="bankDraftPolicyDetails.length === 0">
          <div class="box col-12 mt-3">
            <p class="pt-3 mb-0 fw-bold" style="color:red">
              No Current <b>Bank Draft</b> Detail Found.
            </p>
          </div>
        </div>
        <div class="row" v-if="bankDraftPolicyDetails.length > 0">
          <div class="box col-lg-4 col-sm-4 mt-3">
            <div class="boxTitleBg">Policy Number</div>
            <div class="boxTextBg fw-bold polnum">
              <p
                class="text-center"
                v-for="(item, index) in bankDraftPolicyDetails"
                :key="index"
              >
                <a href="javascript:void(0);" @click="getPolicyBankDraftBillInfo(item?.PolicyNumber)">{{ item?.PolicyNumber }}</a>
              </p>
            </div>
          </div>
          <div class="box col-lg-4 col-sm-4 mt-3">
            <div class="boxTitleBg">Paid To Date</div>
            <div class="boxTextBg dateTxt">
              <p
                class="text-center"
                v-for="(item, index) in bankDraftPolicyDetails"
                :key="index"
              >
                {{ formatDateValue(item?.PaidTo_Date) }}
              </p>
            </div>
          </div>
          <div class="box col-lg-4 col-sm-4 mt-3">
            <div class="boxTitleBg">Mode Premium</div>
            <div class="boxTextBg premium">
              <p v-for="(item, index) in bankDraftPolicyDetails" :key="index">
                ${{ item?.Modal_Premium.toFixed(2) }}
              </p>
            </div>
          </div>
        </div>
      </div>
      <div
        class="govt-allotment col-sm-7 col-lg-7 mt-3"
        v-if="blockType === 'showGovtAllotmentDetails'"
      >
        <div class="row">
          <h5 class="title">Government Allotment</h5>
        </div>
        <div class="mainTitleBg text-start">GOVERNMENT ALLOTMENT INFORMATION</div>
        <div class="mainBoxBg mainBoxRightBg p-3">
          <div class="row">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label>Payer:</label>
            </div>
            <div class="box col-lg-8 col-sm-8 text-end textBG">
              {{ policyBankDraftBill[0]?.Allotment_Payor ? policyBankDraftBill[0]?.Allotment_Payor : 'Allotment Not Received' }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-lg-3 col-sm-3 text-end">
              <label>Company:</label>
            </div>
            <div class="box col-lg-8 col-sm-8 text-end textBG">
              {{ policyBankDraftBill[0]?.CompanyName }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-lg-3 col-sm-3 text-end">
              <label>Policies on Allotment:</label>
            </div>
            <div class="box col-lg-8 col-sm-8 text-end textBG">
              {{ govtAllotmentDetails?.length }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-lg-3 col-sm-3 text-end">
              <label>Allotment Required:</label>
            </div>
            <div class="box col-lg-2 col-sm-2 text-end textBG">
              ${{ totalDraftPremium }}
            </div>

            <div class="box col-lg-4 col-sm-4 text-end">
              <label>Date Received:</label>
            </div>
            <div class="box col-lg-2 col-sm-2 text-end textBG">
              {{ formatDateValue(policyBankDraftBill[0]?.Allotment_Cycle_Date) }}
            </div>
          </div>

          <div class="row mt-2">
            <div class="box col-lg-3 col-sm-3 text-end">
              <label>Amount Received:</label>
            </div>
            <div class="box col-lg-2 col-sm-2 text-end textBG">
              ${{ parseInt(policyBankDraftBill[0]?.Allotment_Amount).toFixed(2) }}
            </div>
            <!-- <div class="box col-lg-4 col-sm-4 text-end">
              <label>Total Draft Premium:</label>
            </div>
            <div class="box col-lg-2 col-sm-2 text-end textBG">
              {{ totalDraftPremium }} $
            </div> -->
          </div>
        </div>

        <div class="titleBGg mt-3">ALLOTMENT POLICY DETAIL</div>
        <p class="pt-3 mb-0 fw-bold">
          Select a policy below to view the policy detail.
        </p>
        <div class="row" v-if="govtAllotmentDetails.length === 0">
          <div class="box col-12">
            <p class="pt-3 mb-0 fw-bold" style="color:red">
              No <b>Current Government Allotment</b> Detail Found.
            </p>
          </div>
        </div>
        <div class="row" v-if="govtAllotmentDetails.length > 1">
          <div class="box col-lg-4 col-sm-4 mt-3">
            <div class="boxTitleBg">Policy Number</div>
            <div class="boxTextBg fw-bold polnum">
              <p
                class="text-center govt-allotment"
                v-for="(item, index) in govtAllotmentDetails"
                :key="index"
              >
                <a v-if="item?.isLink" href="javascript:void(0);" @click="getPolicyBankDraftBillInfo(item?.PolicyNumber)">{{ item?.PolicyNumber }}</a>
                <span v-if="!item?.isLink">{{ item?.PolicyNumber }}</span>
              </p>
            </div>
          </div>
          <div class="box col-lg-4 col-sm-4 mt-3">
            <div class="boxTitleBg">Paid To Date</div>
            <div class="boxTextBg dateTxt">
              <p
                class="text-center"
                v-for="(item, index) in govtAllotmentDetails"
                :key="index"
              >
                {{ formatDateValue(item?.PaidTo_Date) }}
              </p>
            </div>
          </div>
          <div class="box col-lg-4 col-sm-4 mt-3">
            <div class="boxTitleBg">Mode Premium</div>
            <div class="boxTextBg premium">
              <p v-for="(item, index) in govtAllotmentDetails" :key="index">
                ${{ item?.Modal_Premium.toFixed(2) }}
              </p>
            </div>
          </div>
        </div>
      </div>
      <div
        class="col-sm-7 col-lg-7 mt-3 agent-bar"
        v-if="blockType === 'showPolicyBankDraftBill'"
      >
        <div class="row">
          <h5 class="title">Policy Information</h5>
        </div>
        <div class="mainTitleBg text-start">BASIC POLICY INFORMATION</div>
        <div class="mainBoxBg mainBoxRightBg p-3">
          <div class="row mt-2">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label>Policy Number:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyBankDraftBill[0]?.PolicyNumber }}
            </div>
            <div class="box col-sm-2 col-lg-2 text-end">
              <label>Company:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyBankDraftBill[0]?.CompanyName }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label> Policy Owner:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyBankDraftBill[0]?.Owner }}
            </div>
            <div class="box col-sm-2 col-lg-2 text-end">
              <label> Issue Date:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ formatDateValue(policyBankDraftBill[0]?.Issue_Date) }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label> Cash With Application:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ formatDateValue(policyBankDraftBill[0]?.Last_Payment_Date) }}
            </div>
            <div class="box col-sm-2 col-lg-2 text-end">
              <label> UnderWriter:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyBankDraftBill[0]?.Allotment_Payor }}
            </div>
          </div>
          <div class="row mt-2">
            <div class="box col-sm-3 col-lg-3 text-end">
              <label> Status:</label>
            </div>
            <div class="box col-sm-3 col-lg-3 text-end textBG">
              {{ policyBankDraftBill[0]?.StatusDesc }}
            </div>
          </div>
        </div>
        <div class="mt-3">
          <div class="mainTitleBg text-start">INSURED INFORMATION</div>
          <div class="mainBoxBg mainBoxRightBg p-3">
            <div class="row mt-2">
              <div class="col-sm-6">
                <div class="row mb-3 mt-2">
                  <div class="box col-sm-6 col-lg-6 text-end">
                    <label> Insured Name:</label>
                  </div>
                  <div class="box col-sm-6 col-lg-6 text-end textBG">
                    {{ policyBankDraftBill[0]?.Insured }}
                  </div>
                </div>
                <div class="row mb-3 mt-2">
                  <div class="box col-sm-6 col-lg-6 text-end">
                    <label> Birth Date:</label>
                  </div>
                  <div class="box col-sm-6 col-lg-6 text-end textBG">
                    {{ formatDateValue(policyBankDraftBill[0]?.DOB) }}
                  </div>
                </div>
                <div class="row mb-3 mt-2">
                  <div class="box col-sm-6 col-lg-6 text-end">
                    <label> Issue Age:</label>
                  </div>
                  <div class="box col-sm-6 col-lg-6 text-end textBG">
                    {{ policyBankDraftBill[0]?.Issue_Age }}
                  </div>
                </div>
              </div>
              <div class="col-sm-6 col-lg-6">
                <div class="row mt-2">
                  <div class="box col-sm-4 col-lg-4 text-end">
                    <label> Address:</label>
                  </div>
                  <div class="box col-sm-6 col-lg-6 text-end textBG">
                    {{ policyBankDraftBill[0]?.Address_1 }}
                    {{ policyBankDraftBill[0]?.Address_2 }}
                    {{ policyBankDraftBill[0]?.Address_3 }}
                    {{ policyBankDraftBill[0]?.Address_4 }}
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="titleBGg mt-3">BENEFICIARIES</div>
        <div class="row">
          <div class="box col-lg-3 col-sm-3 mt-3">
            <div class="boxTitleBg">TYPE</div>
            <div class="boxTextBg fw-bold">
              <p class="text-center"
                v-for="(item, index) in policyBeneficiary"
                :key="index"
              ><a href="#">
                {{ item?.BeneType }}
              </a></p>
            </div>
          </div>
          <div class="box col-lg-4 col-sm-4 mt-3">
            <div class="boxTitleBg">NAME</div>
            <div class="boxTextBg name">
              <p class="text-center"
                v-for="(item, index) in policyBeneficiary"
                :key="index"
              ><a href="#">
                {{ item?.Name }}
              </a></p>
            </div>
          </div>
          <div class="box col-lg-5 col-sm-5 mt-3">
            <div class="boxTitleBg">RELATIONSHIP</div>
            <div class="boxTextBg rel">
              <p class="text-center"
                v-for="(item, index) in policyBeneficiary"
                :key="index"
              ><a href="#">
                {{ item?.BeneDesc }}
              </a></p>
            </div>
          </div>
        </div>
        <div class="titleBGg mt-3">
          COVERAGE INFORMATON as of
          {{ formatDateValue(policyBankDraftBill[0]?.Cycle_Date) }}
        </div>
        <div class="row coverageInfo">
          <div class="box col-lg-6 col-sm-6 mt-3">
            <div class="boxTitleBg">DESCRIPTION</div>
            <div class="boxTextBg fw-bold">
              <p v-for="(item, index) in policyInfoCoverageData" :key="index">
                <a href="javascript:void(0)">{{ item?.CovDesc }}</a>
              </p>
            </div>
          </div>
          <div class="box col-lg-3 col-sm-3 mt-3">
            <div class="boxTitleBg">CURRENT FACE</div>
            <div class="boxTextBg current">
              <p
                class="text-center"
                v-for="(item, index) in policyInfoCoverageData"
                :key="index"
              >
                {{ item?.Face_Amount }}
              </p>
            </div>
          </div>
          <div class="box col-lg-3 col-sm-3 mt-3">
            <div class="boxTitleBg">MONTHLY PREMIUM</div>
            <div class="boxTextBg premium">
              <p v-for="(item, index) in policyInfoCoverageData" :key="index">
                ${{ item?.Annual_Premium.toFixed(2) }}
              </p>
            </div>
          </div>
        </div>
        <div class="row">
          <p class="box col-sm-12 text-end pr-2">
            <strong>Total: ${{ totalAnnualPremium }}</strong>
          </p>
        </div>
      </div>
    </div>
    <div class="row justify-around" v-if="blockType === 'showPolicyFull'">
      <div class="box col-sm-12 col-lg-12 mt-3">
        <div class="titleBGg mt-3">PENDING REQUIREMENTS</div>
        <div class="row coverageInfo">
          <div class="box col-lg-8 col-sm-8 mt-3">
            <div class="boxTitleBg">DESCRIPTION</div>
            <div class="boxTextBg fw-bold">
              <p v-for="(item, index) in policyInfoNotesData" :key="index">
                {{ item?.NoteMessage }}
              </p>
            </div>
          </div>
          <div class="box col-lg-2 col-sm-2 mt-3">
            <div class="boxTitleBg">SATISFIED</div>
            <div class="boxTextBg current text-center">
              <p
                class="text-center"
                v-for="(item, index) in policyInfoNotesData"
                :key="index"
              >
                {{ item?.NoteSatisfied === "Y" ? "YES" : "NO" }}
              </p>
            </div>
          </div>
          <div class="box col-lg-2 col-sm-2 mt-3">
            <div class="boxTitleBg">DATE REQUESTED</div>
            <div class="boxTextBg premium">
              <p
                class="text-center"
                v-for="(item, index) in policyInfoNotesData"
                :key="index"
              >
                {{ formatDateValue(item?.DateRequest) }}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </q-page>
</template>

<script lang="ts">
import { date } from 'quasar'
import UserServices from '../api/services/UserServices.js'
import { defineComponent, onMounted } from 'vue'

export default defineComponent({
  name: 'IndexPage',
  components: {},
  props: {},
  data () {
    return {
      globalErrorMessage: 'Currently no error messages.',
      isLoading: false,
      blockType: '',
      agentInfoData: '',
      policyInfoData: '',
      policyInfoNotesData: '',
      policyInfoCoverageData: '',
      bankDraftData: '',
      bankDraftPolicyDetails: [],
      govtAllotmentDetails: [],
      policyBankDraftBill: {},
      policyBeneficiary: '',
      agentNumberTxt: '',
      policyNumberTxt: '',
      showGovtAllotmentDetails: false,
      showBankDetails: false,
      showPolicyDetails: false,
      showPolicyFull: false,
      showPolicyBankDraftBill: false,
      isPolicyNumberEmpty: false,
      totalAnnualPremium: 0,
      totalDraftPremium: '',
      totalPoliciesOnDraft: 0
    }
  },
  methods: {
    getLoggedInUserId () {
      UserServices.getLoggedInUserId()
        .then((r) => {
          console.log(r + 'Api response to be binded...')
        })
        .catch((e) => {
          console.log(e)
        })
    },
    setLoader (flag) {
      this.isLoading = flag
    },
    getTotalBankDraftPremium (data) {
      let totalPremium = 0
      data.forEach((premium) => {
        totalPremium = totalPremium + premium.Modal_Premium
      })
      this.totalDraftPremium = totalPremium.toFixed(2)
    },
    getTotalAnnualPremium (data) {
      let totalPremium = 0
      data.forEach((premium) => {
        totalPremium = totalPremium + premium.Annual_Premium
      })
      this.totalAnnualPremium = totalPremium
    },
    formatDateValue (d) {
      return date.formatDate(d, 'MM/DD/YYYY')
    },
    getAgentInfo () {
      const data = { AgentNumber: this.agentNumberTxt }
      this.setLoader(true)
      UserServices.getAgentInfo(data)
        .then((response) => {
          this.setLoader(false)
          this.agentInfoData = response?.data
          if (this.agentInfoData.length > 0) {
            this.setBlockType('showPolicyDetails')
          } else {
            this.setBlockType('')
          }
        })
        .catch((e) => {
          this.setBlockType('')
          console.log(e)
        })
    },
    setBlockType (type) {
      this.blockType = type
    },
    getPendingPolicyInfo (policyNumber) {
      const data = { PolicyNumber: policyNumber }
      this.setLoader(true)
      UserServices.getPendingPolicyInfo(data)
        .then((response) => {
          this.setLoader(false)
          this.policyInfoData = response?.data
          this.getPendingPolicyNotesInfo(policyNumber)
          if (this.policyInfoData.length > 0) {
            this.setBlockType('showPolicyFull')
          } else {
            this.setBlockType('showPolicyDetails')
          }
        })
        .catch((e) => {
          console.log(e)
        })
    },
    getPendingPolicyNotesInfo (policyNumber) {
      const data = { PolicyNumber: policyNumber }
      this.setLoader(true)
      UserServices.getPendingPolicyNotesInfo(data)
        .then((response) => {
          this.setLoader(false)
          this.getPendingPolicyCoverageInfo(policyNumber)
          this.policyInfoNotesData = response?.data
        })
        .catch((e) => {
          console.log(e)
        })
    },
    getPendingPolicyCoverageInfo (policyNumber) {
      const data = { PolicyNumber: policyNumber }
      this.setLoader(true)
      UserServices.getPendingPolicyCoverageInfo(data)
        .then((response) => {
          this.setLoader(false)
          this.policyInfoCoverageData = response?.data
          this.getTotalAnnualPremium(this.policyInfoCoverageData)
        })
        .catch((e) => {
          console.log(e)
        })
    },
    getBankDraftInfo (type, d) {
      const data = { stype: type, polno: d.PolicyNumber, company: d.Company ? d.Company : '' }
      if (d.Bill_Type === 'GA') {
        this.getPolicyGovAllotmentInfo(5, d.PolicyNumber, this.policyBankDraftBill[0] ? this.policyBankDraftBill[0].Company : '')
        this.setBlockType('showGovtAllotmentDetails')
      }
      if (d.Bill_Type === 'BB') {
        UserServices.getBankDraft(data)
          .then((response) => {
            this.setBlockType('showBankDetails')
            this.setLoader(false)
            if (type === '2') {
              this.bankDraftData = response?.data
            }
            if (type === '3') {
              // need to calculate based on status 00, 12, 13
              this.bankDraftPolicyDetails = response?.data
              if (this.bankDraftPolicyDetails ? this.bankDraftPolicyDetails.length > 0 : false) {
                let pCount = 0
                const pArray = []
                this.bankDraftPolicyDetails.forEach((bDraft) => {
                  if (bDraft?.Status === '00' || bDraft?.Status === '12' || bDraft?.Status === '13') {
                    pCount = pCount + 1
                    pArray.push(bDraft)
                  }
                })
                this.getTotalBankDraftPremium(pArray)
                this.bankDraftPolicyDetails = pArray
                // this.getTotalBankDraftPremium(this.bankDraftPolicyDetails)
              }
            }
          })
          .catch((e) => {
            console.log(e)
          })
      }
    },
    getPolicyBankDraftBillInfo (policyNumber) {
      const data = { Stype: '1', Polno: policyNumber }
      this.setLoader(true)
      UserServices.getPolicyBankDraftBill(data)
        .then((response) => {
          this.setLoader(false)
          this.policyBankDraftBill = response?.data
          this.getPolicyBeneficiaries(policyNumber)
          this.setBlockType('showPolicyBankDraftBill')
        })
        .catch((e) => {
          console.log(e)
        })
    },
    getPolicyBeneficiaries (policyNumber) {
      const data = { Polno: policyNumber }
      this.setLoader(true)
      UserServices.getPolicyBeneficiary(data)
        .then((response) => {
          this.setLoader(false)
          this.policyBeneficiary = response?.data
          this.getPendingPolicyCoverageInfo(policyNumber)
          // this.getPolicyGovAllotmentInfo(3, policyNumber, this.policyBankDraftBill[0] ? this.policyBankDraftBill[0].Company : '')
          // this.getPolicyGovAllotmentInfo(5, policyNumber, this.policyBankDraftBill[0] ? this.policyBankDraftBill[0].Company : '')
        })
    },
    getPolicyInqVerifyInfo (policyNumber) {
      if (this.policyNumberTxt !== '') {
        this.isPolicyNumberEmpty = false
        const data = { ID_User: '369', PolicyNumber: policyNumber }
        this.setLoader(true)
        UserServices.getPolicyInqVerify(data)
          .then((response) => {
            this.setLoader(false)
            if (response && response.data[0].ServicingAgentInd === 'Y') {
              this.getPolicyBankDraftBillInfo(policyNumber)
            } else {
              this.setBlockType('')
              this.globalErrorMessage = 'Invalid policy number.'
            }
            // check response and check the flag servicingAgentIndicator & then call the below API
            console.log('-----')
            console.log(response)
            console.log('*****')
          })
      } else {
        this.isPolicyNumberEmpty = true
        console.log('Empty policy field')
      }
    },
    getPolicyGovAllotmentInfo (type, policyNumber, comp) {
      const data = { stype: type, polno: policyNumber, ssn: '000000000', company: comp }
      this.setLoader(true)
      UserServices.getPolicyGovAllotment(data)
        .then((response) => {
          this.setLoader(false)
          console.log(response)
          this.govtAllotmentDetails = response?.data
          if (this.govtAllotmentDetails ? this.govtAllotmentDetails.length > 0 : false) {
            let pCount = 0
            const pArray = []
            this.govtAllotmentDetails.forEach((bDraft) => {
              if (bDraft?.Status === '00' || bDraft?.Status === '12' || bDraft?.Status === '13') {
                pCount = pCount + 1
                pArray.push(bDraft)
              }
            })
            pArray.forEach((govtPol, i) => {
              this.checkAgent(govtPol.PolicyNumber, govtPol.Writing_Agent, i)
            })
            this.getTotalBankDraftPremium(pArray)
            this.govtAllotmentDetails = pArray
          }
        })
    },
    checkAgent (polNum, agentNum, index) {
      const data = { PolicyNumber: polNum, AgentNumber: agentNum, ServicingAgentInd: 'Y' }
      this.setLoader(true)
      UserServices.isAgentServicingAgent(data)
        .then((res) => {
          this.setLoader(false)
          if (res.data[0].ServicingAgentInd === 'Y') {
            this.govtAllotmentDetails[index].isLink = true
          } else {
            this.govtAllotmentDetails[index].isLink = false
          }
        })
        .catch((e) => {
          console.log(e)
        })
    }
  },
  mounted () {
    this.getLoggedInUserId()
  }
})
onMounted(async () => {
  console.log()
  // Test Policy number - A005604937
})
</script>
