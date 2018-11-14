const appConsts = {
    userManagement: {
        adminUserName: 'admin'
    },
    validationRules: {
        required: (v: any) => !!v || 'This field is required.', // todo: translate here
        email: (v: any) => /.+@.+/.test(v) || 'E-mail must be valid.' // todo: translate here
    },
    baseApiUrl: 'https://localhost:44339'
};

export default appConsts;