import IdentityService from "@/core/services/identity/identity-service";
import {required} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import {defineComponent, reactive, ref} from "vue";
import {useRouter} from 'vue-router'

export default defineComponent({
    name: "LoginComponent",
    setup() {
        const loginInput = reactive({} as ILoginInput);
        let submitted = ref(false);
        let router = useRouter();

        const rules = {
            userNameOrEmail: {required},
            password: {required}
        };

        const v$ = useVuelidate(rules, loginInput);

        const handleSubmit = (isFormValid: boolean) => {
            submitted.value = true;
            
            if (!isFormValid) {
                return;
            }
            
            IdentityService.login(loginInput).then(() => {
                router.push("/admin/home");
            }).catch((error) => {
                return;
            });
        };

        function onForgotPasswordClick() {
            router.push("/identity/forgotPassword");
        }

        function onRegisterClick() {
            router.push("/identity/register");
        }
        
        return {
            loginInput,
            v$,
            submitted,
            handleSubmit,
            onForgotPasswordClick,
            onRegisterClick
        }
    }
});