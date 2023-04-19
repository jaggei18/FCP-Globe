import APISettings from '../apiconfig.js'

class UserServices {
  async getLoggedInUserId () {
    try {
      console.log('Need to add API...')
      // return await APISettings.post('InqVerify', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async isAgentServicingAgent (data) {
    try {
      return await APISettings.post('CheckAgent', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getAgentInfo (data) {
    try {
      return await APISettings.post('AgentInfo', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getPendingPolicyInfo (data) {
    try {
      return await APISettings.post('Select_PendingInf', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getPendingPolicyNotesInfo (data) {
    try {
      return await APISettings.post('Select_PendingNotes', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getPendingPolicyCoverageInfo (data) {
    try {
      return await APISettings.post('Coverage', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getBankDraft (data) {
    try {
      return await APISettings.post('Select_BankDraft', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getPolicyBankDraftBill (data) {
    try {
      return await APISettings.post('Select_PolicyInf', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getPolicyBeneficiary (data) {
    try {
      return await APISettings.post('Select_Beneficiary', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getPolicyInqVerify (data) {
    try {
      return await APISettings.post('InqVerify', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }

  async getPolicyGovAllotment (data) {
    try {
      return await APISettings.post('Select_GovAllotment', JSON.stringify(data))
    } catch (error) {
      console.error(error)
    }
  }
}

export default new UserServices()
