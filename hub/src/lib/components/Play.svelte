<script lang="ts">
	import type { ModalSettings, ModalStore } from '@skeletonlabs/skeleton';
	import { getModalStore } from '@skeletonlabs/skeleton';

	import { FF_WEBGL_URL } from '$lib/constants';

	export let scenario: string;
	export let button: boolean = true;

	const modalStore: ModalStore = getModalStore();

	function modalPlay(): void {
		const modal: ModalSettings = {
			type: 'alert',
			buttonTextCancel: 'Close',
            modalClasses: '',
			body: ` 
				<iframe allowfullscreen id="iframe" height="650" class="w-full" src="${FF_WEBGL_URL}?scenario=${scenario}" />
            `
		};
		modalStore.trigger(modal);
    }
</script>

{#if button}
	<a on:click={modalPlay} class="btn variant-filled-primary cursor-pointer">▶ Explore</a>	
{:else}
	<a on:click={modalPlay} class="text-surface-500 cursor-pointer">▶</a>
{/if}

