import {defineComponent, reactive, ref} from "vue";
import {useRouter} from "vue-router";
import {email, required} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import IdentityService from "@/core/services/identity/identity-service";
import {IRegisterInput} from "@/core/services/identity/models/register-input";

export default defineComponent({
    name: "RegisterComponent",
    setup() {
        const registerInput = reactive({} as IRegisterInput);
        let submitted = ref(false);
        let registerComplete = ref(false);
        let registerCompleteMessage = ref("");
        let router = useRouter();

        const rules = {
            userName: {required},
            password: {required},
            passwordRepeat: {required},
            email: {required, email},
        };

        const v$ = useVuelidate(rules, registerInput);

        const handleSubmit = (isFormValid: boolean) => {
            submitted.value = true;

            if (!isFormValid) {
                return;
            }

            IdentityService.register(registerInput).then(() => {
                // TODO: localize
                registerCompleteMessage.value = "RegistrationSuccessful";
                registerComplete.value = true;
            }).catch((error) => {
                return;
            });
        };

        function onLoginClick() {
            router.push("/identity/login");
        }

        return {
            registerInput: registerInput,
            v$,
            submitted,
            handleSubmit,
            onLoginClick,
            registerComplete,
            registerCompleteMessage,
        }
    }
});