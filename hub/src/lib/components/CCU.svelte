<script lang="ts">
	import type { ModalSettings, ModalStore } from '@skeletonlabs/skeleton';
	import { getModalStore } from '@skeletonlabs/skeleton';

    const modalStore : ModalStore = getModalStore();

	function modalCCU(): void {
		const modal: ModalSettings = {
			type: 'alert',
			buttonTextCancel: 'Sluit',
			title: 'Creative Coding Utrecht',
			body: `            
            <a class="anchor" href="https://www.creativecodingutrecht.nl/" target="_blank">Creative Coding Utrecht</a> (CCU) is a platform 
            for digital creativity. CCU supports creators, connects organizations and transcends sectors, inspires audiences and shares 
            knowledge with hobbyists and ICT professionals.
            <br /><br />
            The Future Frictions Hub is developed by <a class="anchor" href="https://www.katpatat.nl/" target="_blank">Neander Giljam</a> and <a class="anchor" href="https://www.appliedcreative.nl/" target="_blank">Niels Janssen</a>.
        `
		};
		modalStore.trigger(modal);
	}
</script>

<button on:click={modalCCU}>
    <img
        class="hidden dark:block"
        src="/images/ccu-white.svg"
        width="50"
        alt="Creative Coding Utrecht"
    />
    <img
        class="block dark:hidden"
        src="/images/ccu-black.svg"
        width="50"
        alt="Creative Coding Utrecht"
    />
</button>
