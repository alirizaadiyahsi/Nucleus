<template>
  <div v-if="!registerComplete" class="card p-fluid">
    <h3 class="text-center">Register</h3>
    <form @submit.prevent="handleSubmit(!v$.$invalid)">
      <div class="field">
        <div class="p-float-label">
          <InputText id="userName" v-model="v$.userName.$model" :class="{'p-invalid':v$.userName.$invalid && submitted}"/>
          <label for="userName" :class="{'p-error':v$.userName.$invalid && submitted}">User Name*</label>
        </div>
        <small v-if="(v$.userName.$invalid && submitted) || v$.userName.$pending.$response" class="p-error">{{ v$.userName.required.$message.replace('Value', 'Name') }}</small>
      </div>
      <div class="field">
        <div class="p-float-label">
          <InputText id="email" v-model="v$.email.$model" :class="{'p-invalid':v$.email.$invalid && submitted}"/>
          <label for="email" :class="{'p-error':v$.email.$invalid && submitted}">Email*</label>
        </div>
        <small v-if="(v$.email.$invalid && submitted) || v$.email.$pending.$response" class="p-error">{{ v$.email.required.$message.replace('Value', 'Name') }}</small>
      </div>
      <div class="field">
        <div class="p-float-label">
          <InputText type="password" id="password" v-model="v$.password.$model" :class="{'p-invalid':v$.password.$invalid && submitted}"/>
          <label for="password" :class="{'p-error':v$.password.$invalid && submitted}">Password*</label>
        </div>
        <small v-if="(v$.password.$invalid && submitted) || v$.password.$pending.$response" class="p-error">{{ v$.password.required.$message.replace('Value', 'Password') }}</small>
      </div>
      <div class="field">
        <div class="p-float-label">
          <InputText type="password" id="passwordRepeat" v-model="v$.passwordRepeat.$model" :class="{'p-invalid':v$.passwordRepeat.$invalid && submitted}"/>
          <label for="passwordRepeat" :class="{'p-error':v$.passwordRepeat.$invalid && submitted}">Password Repeat*</label>
        </div>
        <small v-if="(v$.passwordRepeat.$invalid && submitted) || v$.passwordRepeat.$pending.$response" class="p-error">{{ v$.passwordRepeat.required.$message.replace('Value', 'Password') }}</small>
      </div>
      <Button type="submit" label="Register" class="mt-2"/>
      <Button label="Login" @click="onLoginClick" class="mt-2 p-button-text p-button-secondary"/>
    </form>
  </div>
  <div v-else>
    <div class="card">
      <h3 class="text-center">Register Complete</h3>
      <div class="p-fluid">
        <Message severity="success" :closable="false">{{ registerCompleteMessage }}</Message>
        <Button label="Login" @click="onLoginClick" class="mt-2 p-button-text p-button-secondary"/>
      </div>
    </div>
  </div>
</template>

<script src="./register.ts"></script>