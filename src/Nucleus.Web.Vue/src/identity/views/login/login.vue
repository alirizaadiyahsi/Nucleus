<template>
    <div class="card p-fluid">
      <h3 class="text-center">Login</h3>
      <form @submit.prevent="handleSubmit(!v$.$invalid)">
        <div class="field">
          <div class="p-float-label">
            <InputText id="userNameOrEmail" v-model="v$.userNameOrEmail.$model" :class="{'p-invalid':v$.userNameOrEmail.$invalid && submitted}"/>
            <label for="userNameOrEmail" :class="{'p-error':v$.userNameOrEmail.$invalid && submitted}">User Name or Email*</label>
          </div>
          <small v-if="(v$.userNameOrEmail.$invalid && submitted) || v$.userNameOrEmail.$pending.$response" class="p-error">{{ v$.userNameOrEmail.required.$message.replace('Value', 'Name') }}</small>
        </div>
        <div class="field">
          <div class="p-float-label">
            <InputText type="password" id="password" v-model="v$.password.$model" :class="{'p-invalid':v$.password.$invalid && submitted}"/>
            <label for="password" :class="{'p-error':v$.password.$invalid && submitted}">Password*</label>
          </div>
          <small v-if="(v$.password.$invalid && submitted) || v$.password.$pending.$response" class="p-error">{{ v$.password.required.$message.replace('Value', 'Password') }}</small>
        </div>
        <Button type="submit" label="Login" class="mt-2"/>
        <span class="p-buttonset">
              <Button label="Forgot Password" @click="onForgotPasswordClick" class="mt-2 p-button-text p-button-secondary"/>
              <Button label="Register" @click="onRegisterClick" class="mt-2 p-button-text p-button-secondary"/>
            </span>
      </form>
    </div>
</template>

<script src="./login.ts"></script>