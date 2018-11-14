const appConsts = {
    userManagement: {
        adminUserName: 'admin'
    },
    validationRules: {
        required: (v: any) => !!v || 'This field is required.',
        email: (v: any) => /.+@.+/.test(v) || 'E-mail must be valid.'
    },
    baseApiUrl: 'https://localhost:44339'
};

export default appConsts;